﻿using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using FlexPro.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// TODO: Migrate this to Mediator Pattern
namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuncionarioController : Controller
{
    private readonly IFuncionarioRepository _repository;

    public FuncionarioController(AppDbContext context)
    {
        _repository = new FuncionarioRepository(context);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionario()
    {
        var employee = await _repository.GetAllAsync();
        return employee == null! ? NotFound() : Ok(employee);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        return employee == null ? BadRequest() : Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Funcionario>> CreateEmploye(Funcionario employee)
    {
        await _repository.SaveOrUpdate(employee);
        return CreatedAtAction(nameof(GetFuncionario), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEmployee(int id, Funcionario employee)
    {
        if (id != employee.Id) return BadRequest();

        try
        {
            await _repository.SaveOrUpdate(employee);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _repository.GetByIdAsync(id) == null) return NotFound();

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmployee(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null) return NotFound();

        await _repository.DeleteAsync(employee);
        return NoContent();
    }
}