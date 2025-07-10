using FlexPro.Api.Domain.Models;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, List<string> cc = null, List<string> bcc = null);
        Task EnviarInformativos(IEnumerable<Informativo> informativos);
    }
}
