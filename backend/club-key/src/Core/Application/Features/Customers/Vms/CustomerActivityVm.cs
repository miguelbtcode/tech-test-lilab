namespace Application.Features.Customers.Vms;

using Domain;
using Domain.Abstractions;

public sealed class CustomerActivityVm
{
    public int Id { get; set; }
    public string Name { get;  set; }= string.Empty;
    public string DocumentNumber { get; set; }= string.Empty;
    public CustomerType Type { get;  set; }
    public string PhoneNumber { get;  set; }= string.Empty;
    public Gender Gender { get;  set; }
    public string Email { get;  set; }= string.Empty;
    public DateOnly? BirthDate { get;  set; }
    public bool IsActive { get; set; }
    public ICollection<Entrance> Entrances { get; set; } = [];
    public ICollection<Exit> Exits { get; set; } = [];
}