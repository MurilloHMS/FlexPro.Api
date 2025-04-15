using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformativoController : ControllerBase
    {
        private readonly InformativoService _service;
        private readonly AppDbContext _context;

        public InformativoController(InformativoService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpPost("upload/nfe")]
        public async Task<IActionResult> UploadNfeData(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest();

            IEnumerable<InformativoNFe> dados = await _service.ReadNfeData(file);
            return dados.Any() ? Ok(dados) : BadRequest("Não foi possivel obter os dados do arquivo");
        }

        [HttpPost("upload/os")]
        public async Task<IActionResult> UploadOsData(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest();

            IEnumerable<InformativoOS> dados = await _service.ReadOsData(file);
            return dados.Any() ? Ok(dados) : BadRequest("Não foi possivel obter os dados do arquivo");      
        }

        [HttpPost("upload/pecasTrocadas")]
        public async Task<IActionResult> UploadPecasTrocadas(IFormFile file)
        {
            if (file ==null || file.Length == 0) return BadRequest();

            IEnumerable<InformativoPecasTrocadas> dados = await _service.ReadPecasTrocadasData(file);
            return dados.Any() ? Ok(dados) : BadRequest("Não foi possivel obter os dados do arquivo");
        }

        [HttpPost("calcular")]
        public async Task<IActionResult> GenerateMetrics([FromBody] DadosRequest dados)
        {
            if( dados == null) return BadRequest("Dados não foram enviados corretamente");

            IEnumerable<Informativo> informativos = await _service.CreateInfoData(dados.InformativoNFes, dados.informativoOs, dados.InformativoPecasTrocadas);

            return informativos.Any() ? Ok(informativos) : BadRequest("Não foi possivel obter os dados");

        }


    }

    public class DadosRequest
    {
        public List<InformativoNFe> InformativoNFes { get; set; }
        public List<InformativoPecasTrocadas> InformativoPecasTrocadas { get; set; }
        public List<InformativoOS> informativoOs { get; set; }
    }
}
