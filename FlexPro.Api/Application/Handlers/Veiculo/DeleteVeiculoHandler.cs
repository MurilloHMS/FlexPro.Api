using FlexPro.Api.Application.Commands.Veiculo;
using FlexPro.Api.Application.Interfaces;
using MediatR;

namespace FlexPro.Api.Application.Handlers.Veiculo
{
    public class DeleteVeiculoHandler : IRequestHandler<DeleteVeiculoCommand>
    {
        private readonly IVeiculoRepository _repo;

        public DeleteVeiculoHandler(IVeiculoRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteVeiculoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetById(request.Id);
            await _repo.Delete(entity);
            return Unit.Value;
        }
    }
}
