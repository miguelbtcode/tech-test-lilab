namespace Application.Features.Customers.Vms;

using Domain;
using Domain.Abstractions;

public sealed record CustomerVm
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public CustomerType Type { get; private set; }
    public string PhoneNumber { get; private set; } = string.Empty;
    public DateOnly? BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public ICollection<Entrance> Entrances { get; private set; } = new List<Entrance>();
    public ICollection<Exit> Exits { get; private set; } = new List<Exit>();
    public int Age { get; set; }
    public bool IsActive { get; set; }
}