namespace Domain.Users;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public override string? PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; } = new List<IdentityUserRole<string>>();
}