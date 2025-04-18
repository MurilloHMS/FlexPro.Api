﻿using FlexPro.Api.Application.DTOs;
using FlexPro.Api.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FlexPro.Api.Application.Commands.Veiculo;
using FlexPro.Api.Application.Queries.Veiculo;

namespace FlexPro.Test.Controllers
{
    public class VeiculoControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly VeiculoController _controller;

        public VeiculoControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new VeiculoController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithList()
        {
            var veiculos = new List<VeiculoDTO>
            {
                new VeiculoDTO { Nome = "Carro 1", Placa = "AAA-1234", Marca = "Ford" },
                new VeiculoDTO { Nome = "Carro 2", Placa = "BBB-5678", Marca = "Chevrolet" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllVeiculosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(veiculos);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(veiculos, okResult.Value);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkWhenFound()
        {
            var dto = new VeiculoDTO { Nome = "Carro", Placa = "AAA-1234", Marca = "Fiat" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetVeiculoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<VeiculoDTO>(okResult.Value);
            Assert.Equal("Carro", value.Nome);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            var command = new CreateVeiculoCommand
            {
                Nome = "Novo Carro",
                Placa = "XYZ-9876",
                Marca = "VW"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateVeiculoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(42);

            var result = await _controller.Create(command);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(42, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContent()
        {
            var command = new UpdateVeiculoCommand
            {
                Id = 1,
                Nome = "Atualizado",
                Placa = "ZZZ-9999",
                Marca = "Hyundai"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateVeiculoCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Update(1, command);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_IfIdMismatch()
        {
            var command = new UpdateVeiculoCommand { Id = 2 };

            var result = await _controller.Update(1, command);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteVeiculoCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
