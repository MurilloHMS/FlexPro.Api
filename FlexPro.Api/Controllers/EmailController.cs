using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using FlexPro.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<ActionResult> SendMailAsync(Email emailData)
        {
            if (emailData == null) return BadRequest("Dados do e-mail são obrigatórios");

            try
            {
                await _emailService.SendEmailAsync(emailData.To, emailData.Subject, emailData.Body, emailData.Cc, emailData.Bcc);
                return Ok("E-mail enviado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao enviar o e-mail: {ex.Message} stack {ex.StackTrace}");
            }
        }
    }
}
