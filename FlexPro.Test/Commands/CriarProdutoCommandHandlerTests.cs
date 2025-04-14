using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexPro.Api.Application.Commands;
using FlexPro.Test.Setup;
using Xunit;
using FluentAssertions;

namespace FlexPro.Test.Commands
{
    public class CriarProdutoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveCriarVeiculoERetornarID()
        {
            var context = TestDbContextFactory.CreateInMemoryContext();
            var handler = new CriarVeiculoCommandHanddler(context);
            var command = new CriarVeiculoCommand
            {
                Nome = "teste",
                Marca = "FIAT",
                Placa = "FFR7092"
            };

            var resultado = await handler.Handle(command, CancellationToken.None);

            resultado.Should().BeGreaterThan(0);
            var veiculoCriado = await context.Veiculo.FindAsync(resultado);
            veiculoCriado.Should().NotBeNull();
            veiculoCriado!.Nome.Should().Be(command.Nome);
        }
    }
}
