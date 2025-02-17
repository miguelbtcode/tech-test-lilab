namespace Application.Contracts.Infrastructure;

using Models.Email;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}