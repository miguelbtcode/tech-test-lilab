namespace Infrastructure.Persistence;

using Domain.Abstractions;
using Domain.Common;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ClubKeyDbContext : IdentityDbContext<User>
{
    public ClubKeyDbContext(DbContextOptions<ClubKeyDbContext> options) : base(options) { }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            switch(entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "system";
                    entry.Entity.IsActive = true;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "system";
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>()
            .HasIndex(c => new { c.DocumentNumber, c.IsActive  })
            .IsUnique();
        
        builder.Entity<Customer>()
            .HasMany(c => c.Entrances)
            .WithOne(e => e.Customer)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Customer>()
            .HasMany(c => c.Exits)
            .WithOne(e => e.Customer)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Entrance>()
            .HasIndex(e => new { e.EntranceTime, e.CustomerId, e.IsActive })
            .IsUnique();
        
        builder.Entity<Exit>()
            .HasIndex(e => new { e.ExitTime, e.CustomerId, e.IsActive })
            .IsUnique();
        
        builder.Entity<Customer>()
            .Property(c => c.Type)
            .HasComment("Specifies if the customer is a visitor or a member => 1: Visitor, 2: Member");
        
        builder.Entity<Customer>()
            .Property(c => c.Gender)
            .HasComment("Specifies if the customer is a Male, Female or other => 1: Male, 2: Female, 3: Other");
        
        builder.Entity<IdentityUserRole<string>>()
            .HasKey(iur => new { iur.UserId, iur.RoleId });
        
        builder.Entity<IdentityUserLogin<string>>()
            .HasKey(iul => new { iul.LoginProvider, iul.ProviderKey });
        
        builder.Entity<IdentityUserToken<string>>()
            .HasKey(iut => new { iut.UserId, iut.LoginProvider, iut.Name });
        
        builder.Entity<User>().Property(x => x.Id).HasMaxLength(36);
        builder.Entity<User>().Property(x => x.NormalizedUserName).HasMaxLength(90);
        builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(36);
        builder.Entity<IdentityRole>().Property(x => x.NormalizedName).HasMaxLength(90);
    }

    public DbSet<Customer>? Customers { get; set; }

    public DbSet<Entrance>? Entrances { get; set; }

    public DbSet<Exit>? Exits { get; set; }
}