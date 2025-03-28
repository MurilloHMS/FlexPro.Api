using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;
using System.Globalization;

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

        public async Task EnviarInformativos(IEnumerable<Informativo> informativos)
        {
            var htmlTemplate = @"
                                <!DOCTYPE html>
                                <html lang=""pt-br"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <title>Proauto Kimium - Métricas Mensais</title>
                                </head>
                                <body style=""margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f4f4f4; text-align: center;"">
                                    <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
                                        <tr>
                                            <td align=""center"">
                                                <!-- Cabeçalho -->
                                                <table role=""presentation"" width=""600"" cellspacing=""0"" cellpadding=""20"" border=""0"" style=""background: #262d61; color: #ffffff; border-radius: 8px 8px 0 0; background-size: 100%;"">
                                                    <tr>
                                                        <td align=""center"">
                                                            <img src=""https://www.proautokimium.com.br/images/2020/11/icone-proauto-150x150.png"" alt=""Proauto Kimium"" width=""60"" height=""60"">
                                                            <h2 style=""margin: 5px 0;"">Métricas Mensais</h2>
                                                            <h3 style=""margin: 5px 0;"">{0}</h3>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <!-- Mensagem de Introdução -->
                                                <table role=""presentation"" width=""600"" cellspacing=""0"" cellpadding=""20"" border=""0"" style=""background-color: #ffffff;"">
                                                    <tr>
                                                        <td align=""center"">
                                                            <p style=""color: #555; font-size: 16px;"">
                                                                Olá, <strong>{1}</strong>! Veja como foi seu desempenho neste mês com a <strong>Proauto Kimium</strong>.
                                                            </p>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <!-- Métricas Principais -->
                                                <table role=""presentation"" width=""600"" cellspacing=""0"" cellpadding=""15"" border=""0"" style=""background-color: #ffffff;"">
                                                    <tr>
                                                        <td align=""center"" width=""33%"" style=""border-right: 1px solid #ddd;"">
                                                            <h1 style=""color: #007bff; margin: 0; font-size: 2rem;"">{2}</h1>
                                                            <p style=""color: #555; font-size: 14px;"">Litros de Produtos Comprados</p>
                                                        </td>
                                                        <td align=""center"" width=""33%"" style=""border-right: 1px solid #ddd;"">
                                                            <h1 style=""color: #007bff; margin: 0; font-size: 2rem;"">{3}</h1>
                                                            <p style=""color: #555; font-size: 14px;"">Notas Fiscais Emitidas</p>
                                                        </td>
                                                        <td align=""center"" width=""33%"">
                                                            <h1 style=""color: #007bff; margin: 0; font-size: 2rem;"">{4}</h1>
                                                            <p style=""color: #555; font-size: 14px;"">Média Dias para atendimento</p>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <!-- Destaques -->
                                                <table role=""presentation"" width=""600"" cellspacing=""0"" cellpadding=""15"" border=""0"" style=""background-color: #ffffff; margin-top: 10px;"">
                                                    <tr>
                                                        <td align=""center"" style=""background-color: #f8f9fa; padding: 15px;"">
                                                            <h3 style=""color: #333; margin: 5px 0;"">📌 Destaque do Mês</h3>
                                                            <p style=""color: #555; font-size: 14px; margin: 5px 0;"">Produto mais comprado: <strong>{5}</strong></p>
                                                            <p style=""color: #555; font-size: 14px; margin: 5px 0;"">Faturamento total: <strong>R$ {6}</strong></p>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <!-- Máquinas Alugadas -->
                                                <table role=""presentation"" width=""600"" cellspacing=""0"" cellpadding=""15"" border=""0"" style=""background-color: #ffffff;"">
                                                    <tr>
                                                        <td align=""center"">
                                                            <h1 style=""color: #007bff; margin: 0; font-size: 2rem;"">{7}</h1>
                                                            <p style=""color: #555; font-size: 14px;"">Quantidade de produtos comprados</p>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <!-- Rodapé -->
                                                <table role=""presentation"" width=""600"" cellspacing=""0"" cellpadding=""15"" border=""0"" style=""background-color: #262d61; color: #ffffff; border-radius: 0 0 8px 8px; margin-top: 20px;"">
                                                    <tr>
                                                        <td align=""center"">
                                                            <img src=""https://www.proautokimium.com.br/images/2020/11/logo-proauto.png"" alt=""Proauto Kimium"" width=""120"" style=""opacity: 0.8;"">
                                                            <p style=""margin: 5px 0; font-size: 14px;"">📞 (11) 99999-9999 | 📧 sac@proautokimium.com.br</p>
                                                            <p style=""margin: 5px 0; font-size: 12px;"">Av João do Prado, 300 - Santo André, SP</p>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                        </tr>
                                    </table>
                                </body>
                                </html>";

            var sb = new StringBuilder();
            foreach( var informativo in informativos)
            {
                var html = string.Format(htmlTemplate,
                    informativo.Mes,
                    informativo.NomeDoCliente,
                    informativo.QuantidadeDeLitros,
                    informativo.QuantidadeNotasEmitidas,
                    informativo.MediaDiasAtendimento,
                    informativo.ProdutoEmDestaque,
                    informativo.FaturamentoTotal.ToString("F2",CultureInfo.InvariantCulture),
                    informativo.QuantidadeDeProdutos

                    );

                await SendEmailAsync(informativo.EmailCliente, $"Informativo Mês {informativo.Mes}", html );
            }
        }
    }
}
