using FlexPro.Api.Data;
using FlexPro.Api.Models;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FlexPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbastecimentoController : ControllerBase
    {
        private readonly AbastecimentoService _service;
        private readonly AppDbContext _context;
        private readonly IAbastecimentoRepository _repository;
        public AbastecimentoController(AppDbContext context, AbastecimentoService service, IAbastecimentoRepository repository)
        {
            _context = context;
            _service = service;
            _repository = repository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest();

            List<Abastecimento> dadosAbastecimento = await _service.ColetarDadosAbastecimento(file);

            if(dadosAbastecimento != null && dadosAbastecimento.Any())
            {
                foreach(var abastecimento in dadosAbastecimento)
                {
                    var departamento = _context.Funcionarios.FirstOrDefault(f => f.Nome.ToUpper().Contains(abastecimento.NomeDoMotorista.ToUpper()));
                    abastecimento.Departamento = departamento != null ? departamento.Departamento : "Sem Departamento";
                }
                try
                {
                    await _repository.AddRangeFuelSupply(dadosAbastecimento);
                }catch(Exception e)
                {
                    return BadRequest($"Ocorreu um erro ao salvar os dados de abastecimento {e.Message}");
                }
            }

            return Ok(dadosAbastecimento);
        }

        [HttpGet]
        public async Task<ActionResult> GetAbastecimento()
        {
            var abastecimento = await _repository.GetFuelSupply();
            return abastecimento != null ? Ok(abastecimento) : NotFound();
        }

        [HttpGet("Calcular/Individual/{data}")]
        public async Task<ActionResult> GetIndividualMetrics(DateTime data)
        {
            var retorno = await _service.CalcularAbastecimentoIndividual(data);
            return retorno != null ? Ok(retorno) : NotFound();
        }

        [HttpGet("Calcular/Setor/{data}")]
        public async Task<ActionResult> GetSetorMetrics(DateTime data)
        {
            var retorno = await _service.CalcularAbastecimentoSetor(data);
            return retorno != null ? Ok(retorno) : NotFound();
        }

        [HttpGet("Calcular/Geral/{data}")]
        public async Task<ActionResult> GetGeralMetrics(DateTime data)
        {
            var retorno = await _service.CalcularAbastecimentoGeral(data);
            return retorno != null ? Ok(retorno) : NotFound();
        }
    }
}
