using FlexPro.Api.Controllers;
using FlexPro.Application.DTOs;
using FlexPro.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace FlexPro.Api.Test.Controllers;

[TestClass]
public class VehicleControllerTests
{
    private VeiculoController _controller = null!;
    private Mock<IMediator> _mediatorMock = null!;

    [TestInitialize]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new VeiculoController(_mediatorMock.Object);
    }

    [TestMethod]
    public async Task Shold_ReturnsOK_When_VehicleList_Are_Valid()
    {
        var veiculos = new List<VeiculoDto>
        {
            new VeiculoDto { Placa = "ABC1234", Nome = "Uno" },
            new VeiculoDto { Placa = "XYZ5678", Nome = "Gol" }
        };

        var response = new FlexPro.Application.UseCases.Vehicles.GetAll.Response(veiculos);
        var result = Result.Success(response);

        _mediatorMock.Setup(m => m.Send(
            It.IsAny<FlexPro.Application.UseCases.Vehicles.GetAll.Command>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(result);
        
        var actionResult = await _controller.GetAll();
        
        Assert.IsInstanceOfType(actionResult, typeof(Ok<FlexPro.Application.UseCases.Vehicles.GetAll.Response>));
        var okResult = actionResult as Ok<FlexPro.Application.UseCases.Vehicles.GetAll.Response>;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(response, okResult.Value);
    }

    [TestMethod]
    public async Task Shold_ReturnsNotFound_When_VehicleList_Are_Not_Valid()
    {
        var error = new Error("404", "Vehicle not found");
        var expectedResult = Result.Failure<FlexPro.Application.UseCases.Vehicles.GetAll.Response>(error);
        
        _mediatorMock.Setup(m => m.Send(It.IsAny<FlexPro.Application.UseCases.Vehicles.GetAll.Command>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);
        
        var actionResult = await _controller.GetAll();
        Assert.IsInstanceOfType(actionResult, typeof(NotFound<Error>));
        var notFoundResult = actionResult as NotFound<Error>;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(error, notFoundResult.Value);
    }
}