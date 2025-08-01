﻿using FlexPro.Domain.Models;
using FlexPro.Domain.Repositories;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorLight;
using SmtpLw;
using SmtpLw.Models;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace FlexPro.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _settings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body, List<string>? cc = null,
        List<string>? bcc = null)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Proauto Kimium", _settings.FromEmail));
        message.To.Add(new MailboxAddress("Destinatário", to));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = body };

        if (cc != null && cc.Any())
            foreach (var email in cc)
                message.Cc.Add(new MailboxAddress("", email));

        if (bcc != null && bcc.Any())
            foreach (var email in bcc)
                message.Bcc.Add(new MailboxAddress("", email));

        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_settings.Username, _settings.Password);
                await client.SendAsync(message);
            }
            catch (SmtpCommandException ex)
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

    public async Task EnviarInformativos(IEnumerable<Informativo> informativos)
    {
        var engine = new RazorLightEngineBuilder()
            .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Infrastructure", "Templates", "Email"))
            .UseMemoryCachingProvider()
            .Build();

        var semaphore = new SemaphoreSlim(100);

        var tasks = informativos.Select(async newsletter =>
        {
            await semaphore.WaitAsync();
            try
            {
                var html = await engine.CompileRenderAsync("Informativo.cshtml", newsletter);
                await SendEmailLocalWebSmtpAsync(newsletter.EmailCliente,
                    $"Resumo Proauto Kimium - {newsletter.Mes}", html);
            }
            finally
            {
                semaphore.Release();
            }
        });
        await Task.WhenAll(tasks);
    }

    public async Task SendEmailLocalWebSmtpAsync(string to, string subject, string body, List<string>? cc = null,
        List<string>? bcc = null)
    {
        var client = new SmtpLwClient("465be8f65256ab11670023b66bee82a2");
        var message = new MessageModel();
        message.Subject = subject;
        message.Body = body;
        message.To = to;
        message.From = Environment.GetEnvironmentVariable("SMTP__FROM_EMAIL");
        message.Headers = new Dictionary<string, string> { { "Content-Type", "text/html" } };

        // if (cc != null && cc.Any())
        // {
        //     message.CarbonCopy.AddRange(cc);
        // }
        //
        // if (bcc != null && bcc.Any())
        // {
        //     message.BlindCarbonCopy.AddRange(bcc);
        // }

        var messageId = await client.SendMessageAsync(message, CancellationToken.None).ConfigureAwait(false);
        Console.WriteLine($@"Message Id: {messageId}");
    }
}