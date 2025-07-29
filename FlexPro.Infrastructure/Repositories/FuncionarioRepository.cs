using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories
{
    public class FuncionarioRepository(AppDbContext context) : Repository<Funcionario>(context),IFuncionarioRepository
    {
        private readonly DbSet<Funcionario> _dbSet = context.Set<Funcionario>();

        public async Task DeleteById(int id)
        {
            var funcionario = _dbSet.FirstOrDefault(f => f.Id == id);
            if (funcionario != null)
            {
                context.Funcionarios.Remove(funcionario);
                await context.SaveChangesAsync();
            }
        }
        public async Task SaveOrUpdate(Funcionario funcionario)
        {
            var employeeFound = await _dbSet.FirstOrDefaultAsync(x => x.Id == funcionario.Id);
            if (employeeFound != null)
            {
                context.Entry(employeeFound).CurrentValues.SetValues(funcionario);
            }
            else
            {
                _dbSet.Add(funcionario);
            }

            await context.SaveChangesAsync();
        }
    }
}
