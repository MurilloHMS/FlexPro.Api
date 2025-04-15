//using FlexPro.Api.Infrastructure.Persistance;
//using FlexPro.Api.Interfaces;
//using FlexPro.Api.Models;
//using FlexPro.Api.Repository;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace FlexPro.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VeiculoController : ControllerBase
//    {
//        private readonly AppDbContext _context;
//        private readonly IVeiculoRepository _veiculoRepository;

//        public VeiculoController(AppDbContext context)
//        {
//            _context = context;
//            _veiculoRepository = new VeiculoRepository(_context);
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVehicle()
//        {
//            var vehicle = await _veiculoRepository.GetAll();
//            return vehicle == null ? NotFound() : Ok(vehicle);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Veiculo>> GetVehicle(int id)
//        {
//            var veiculo = await _veiculoRepository.GetById(id);
//            return veiculo == null ? NotFound() : Ok(veiculo);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Veiculo>> CreateVehicle(Veiculo veiculo)
//        {
//            await _veiculoRepository.UpdateOrInsert(veiculo);
//            return CreatedAtAction(nameof(GetVehicle), new { id = veiculo.Id }, veiculo);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateVehicle(int id, Veiculo veiculo)
//        {
//            if (id != veiculo.Id)
//            {
//                return BadRequest();
//            }

//            try
//            {
//                await _veiculoRepository.UpdateOrInsert(veiculo);
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (await _veiculoRepository.GetById(id) == null)
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteVehicle(int id)
//        {
//            var veiculo = await _veiculoRepository.GetById(id);
//            if (veiculo == null)
//            {
//                return NotFound();
//            }

//            await _veiculoRepository.Delete(veiculo);
//            return NoContent();
//        }
//    }
//}