﻿using FlexPro.Api.Models;

namespace FlexPro.Api.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, List<string> cc = null, List<string> bcc = null);
        Task EnviarInformativos(IEnumerable<Informativo> informativos);
    }
}
