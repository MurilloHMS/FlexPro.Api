using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using FlexPro.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Controllers
{
    public class FuncionarioControll : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFuncionarioRepository _repository;

        public FuncionarioControll(AppDbContext context)
        {
            _context = context;
            _repository = new FuncionarioRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionario()
        {
            var employee = await _repository.GetAll();
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var employee = await _repository.GetById(id);
            return employee == null ? BadRequest() : Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Funcionario>> CreateEmploye(Funcionario employee)
        {
            await _repository.SaveOrUpdate(employee);
            return CreatedAtAction(nameof(GetFuncionario), new {id = employee.Id}, employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id,  Funcionario employee)
        {
            if(id != employee.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.SaveOrUpdate(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _repository.GetById(id) == null)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await _repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            await _repository.Delete(employee);
            return NoContent();
        }
    }
}
