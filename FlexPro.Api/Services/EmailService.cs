using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace FlexPro.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _settings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body, List<string> cc = null, List<string> bcc = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Proauto Kimium", _settings.FromEmail));
            message.To.Add(new MailboxAddress("Destinatário", to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            if (cc != null && cc.Any())
            {
                foreach (var email in cc)
                {
                    message.Cc.Add(new MailboxAddress("", email));
                }
            }

            if (bcc != null && bcc.Any())
            {
                foreach (var email in bcc)
                {
                    message.Bcc.Add(new MailboxAddress("", email));
                }
            }
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_settings.Username, _settings.Password);
                    await client.SendAsync(message);
                }catch (SmtpCommandException ex)
                {
                    _logger.LogError($"Erro ao enviar o email: {ex.Message}");
                    throw new Exception($"Erro ao enviar email: {ex.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro inesperado: {ex.Message}");
                    throw new Exception($"Erro inesperado: {ex.Message}");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}
