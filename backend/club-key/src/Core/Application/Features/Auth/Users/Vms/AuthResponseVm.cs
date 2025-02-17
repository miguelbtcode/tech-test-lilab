namespace Application.Features.Auth.Users.Vms;

public sealed record AuthResponseVm
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string?  PhoneNumber { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<string>? Roles {get;set;}
}