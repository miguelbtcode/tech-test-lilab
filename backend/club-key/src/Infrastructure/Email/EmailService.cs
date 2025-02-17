namespace Infrastructure.Email;

using System.Net;
using Application.Contracts.Infrastructure;
using Application.Models.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService : IEmailService
{
    public EmailSettings _emailSettings { get; }

    public ILogger<EmailService> _logger { get; }

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);

        var subjet = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subjet, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        if (response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.OK)
            return true;

        _logger.LogError("The email could not be sent");
        return false;
    }
}