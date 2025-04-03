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
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>Informativo</title>
                                    <style>
                                        :root{
                                            --green-color: #60bda9;
                                            --blue-color: #252d61;
                                        }
                                        body {
                                            font-family: Arial, sans-serif;
                                            margin: 0;
                                            padding: 0;
                                            display: flex;
                                            justify-content: center;
                                            align-items: center;
                                            height: 100vh;
                                            background: #f4f4f4;
                                        }
                                        .container {
                                            width: 90%;
                                            max-width: 600px;
                                            background: white;
                                            border-radius: 45px;
                                            border: 3px solid var(--blue-color);
                                            text-align: center;
                                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.4);
                                        }
                                        .header{
                                            display: flex;
                                            justify-content: center;
                                            padding: 8px 25px;
                                        }
                                        .header img {
                                            width: 60px;
                                        }
                                        .header h2 {
                                            margin: 10px 0;
                                            text-transform: uppercase;
                                            margin-left: 5px;
                                            font-size: 2.4rem;
                                        }
                                        .blue{
                                            color: var(--blue-color);
                                            font-weight: 800;
                                            margin-right: 4px;
                                        }
                                        .green{
                                            color: var(--green-color);
                                            font-weight: 800;
                                        }
                                        .presentation h3 {
                                            margin: 0;
                                            color: var(--blue-color);
                                            font-weight: 800;
                                        }
                                        .presentation p {
                                            color: var(--blue-color);
                                            font-size: 16px;
                                            padding: 0 120px;
                                        }
                                        .presentation-body {
                                            display: flex;
                                            flex-wrap: wrap;
                                            justify-content: space-between;
                                            margin-top: 40px;
                                        }
                                        .item {
                                            width: 33%;
                                            margin-top: 20px;
                                        }
                                        .item-title {
                                            color: var(--green-color);
                                            font-size: 1.6rem;
                                            font-weight: 800;
                                            margin: 0;
                                            text-transform: uppercase;
                                            padding: 0 12px;
                                        }
                                        .item-info {
                                            color: var(--blue-color);
                                            font-size: 16px;
                                            padding: 0 25px;
                                            font-weight: bold;
                                        }
                                        .divider{
                                            display: flex;
                                            margin-left: auto;
                                            margin-right: auto;
                                            width: 90%;
                                            height: 2px;
                                            background-color: #bdc0cf;
                                        }
                                        .footer {
                                            background: var(--blue-color);
                                            color: white;
                                            padding: 10px;
                                            border-radius: 0 0 40px 40px;
                                            font-size: 14px;
                                            margin-top: 18px;
                                        }
                                        .footer a {
                                            color: #ddd;
                                            text-decoration: none;
                                        }
                                        .footer img {
                                            width: 15px;
                                            margin-right: 5px;
                                        }
                                    </style>
                                </head>
                                <body>
                                    <div class=""container"">
                                        <div class=""header"">
                                            <img src=""https://media.licdn.com/dms/image/v2/D4D0BAQFCj_lQSEJtrw/company-logo_200_200/company-logo_200_200/0/1723201815949/proauto_kimium_logo?e=2147483647&v=beta&t=PwLlkYogzj6nuWivG_03yRCZQxnPyjZQ3BNYncTycOI"" alt=""Proauto Kimium"">
                                            <h2><strong class=""blue"">Proauto</strong><strong class=""green"">Kimium</strong></h2>
                                        </div>
                                        <div class=""presentation"">
                                            <h3>{0} - 2025</h3>
                                            <p>Olá, <strong>{1}</strong>! Veja como foi seu desempenho neste mês com a <strong>Proauto Kimium</strong>.</p>
                                        </div>
                                        <div class=""presentation-body"">
                                            <div class=""item""><h1 class=""item-title"">{5}</h1><p class=""item-info"">Produto mais comprado</p></div>
                                            <div class=""item""><h1 class=""item-title"">{7}</h1><p class=""item-info"">Qtd. de produtos comprados</p></div>
                                            <div class=""item""><h1 class=""item-title"">{2}</h1><p class=""item-info"">Litros comprados</p></div>
                                            <div class=""divider""></div>
                                            <div class=""item""><h1 class=""item-title"">2</h1><p class=""item-info"">Manutenções Realizadas</p></div>
                                            <div class=""item""><h1 class=""item-title"">{8}</h1><p class=""item-info"">Valor das peças trocadas</p></div>
                                            <div class=""item""><h1 class=""item-title"">{4}</h1><p class=""item-info"">Média de dias para atendimento</p></div>
                                            <div class=""divider""></div>
                                            <div class=""item""><h1 class=""item-title"">{6}</h1><p class=""item-info"">Faturamento Mensal</p></div>
                                            <div class=""item""><h1 class=""item-title"">15</h1><p class=""item-info"">Produtos Diferentes</p></div>
                                            <div class=""item""><h1 class=""item-title"">{3}</h1><p class=""item-info"">Notas Fiscais Emitidas</p></div>
                                        </div>
                                        <div class=""footer"">
                                            <p><img src=""https://images.icon-icons.com/1294/PNG/512/2362137-chat-media-whatsapp_85529.png"" alt=""""><a href=""https://wa.me/5511957782766"" target=""_blank"">(11) 9 5778-2766</a> | 📧 <a href=""mailto:sac@proautokimium.com.br"">sac@proautokimium.com.br</a></p>
                                            <p>Av. João do Prado, 300 - Santo André, SP</p>
                                        </div>
                                    </div>
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
                    informativo.QuantidadeDeProdutos,
                    informativo.ValorDePeçasTrocadas
                    );

                await SendEmailAsync(informativo.EmailCliente, $"Informativo Mês {informativo.Mes}", html );
            }
        }
    }
}
