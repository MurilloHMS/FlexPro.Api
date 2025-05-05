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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            return await _context.Cliente.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }
        [AllowAnonymous]
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
