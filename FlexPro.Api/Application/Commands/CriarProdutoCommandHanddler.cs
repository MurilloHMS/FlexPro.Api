using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using MediatR;

namespace FlexPro.Api.Application.Commands
{
    public class CriarProdutoCommandHanddler : IRequestHandler<CriarVeiculoCommand, int>
    {
        private readonly AppDbContext _context;

        public CriarProdutoCommandHanddler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CriarVeiculoCommand request, CancellationToken cancellationToken)
        {
            var veiculo = new Veiculo { Nome = request.Nome, Placa = request.Placa, Marca = request.Marca };
            _context.Veiculo.Add(veiculo);
            await _context.SaveChangesAsync();
            return veiculo.Id;
        }
    }
}
