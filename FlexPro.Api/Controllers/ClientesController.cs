using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace FlexPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpPost("AllEmployees")]
        public async Task<IActionResult> CreateEmployeeData(IFormFile file)
        {
            var clientes = new List<Cliente>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (XLWorkbook wb = new XLWorkbook(stream))
                {
                    var planilha = wb.Worksheets.FirstOrDefault();
                    var dados = planilha.RowsUsed().Skip(1).Select(row => new Cliente
                    {
                        CodigoSistema = row.Cell(1).TryGetValue<string>(out var codigoSistemaCliente)
                            ? codigoSistemaCliente
                            : default,
                        Nome = row.Cell(2).TryGetValue<string>(out var nomeParceiro) ? nomeParceiro : default,
                        Email = row.Cell(3).TryGetValue<string>(out var email) ? email : default,
                        Status = "true"
                    }).ToList();
                    
                    clientes.AddRange(dados);
                }
            }
            
            _context.Cliente.AddRange(clientes);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
