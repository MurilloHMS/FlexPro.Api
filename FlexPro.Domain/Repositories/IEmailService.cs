using FlexPro.Domain.Models;

namespace FlexPro.Domain.Repositories
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, List<string> cc = null, List<string> bcc = null);
        Task EnviarInformativos(IEnumerable<Informativo> informativos);
    }
}
