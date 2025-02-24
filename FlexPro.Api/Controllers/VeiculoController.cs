using FlexPro.Api.Data;
using FlexPro.Api.Models;
using FlexPro.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoController(AppDbContext context)
        {
            _context = context;
            _veiculoRepository = new VeiculoRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVehicle()
        {
            var vehicle = await _veiculoRepository.GetAll();
            return vehicle == null ? NotFound() : Ok(vehicle);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetVehicle(int id)
        {
            var veiculo = await _veiculoRepository.GetById(id);
            return veiculo == null ? NotFound() : Ok(veiculo);
        }
    }
}