using FlexPro.Api.Controllers;
using FlexPro.Application.DTOs;
using FlexPro.Application.UseCases.Vehicles.GetAll;
using FlexPro.Application.UseCases.Vehicles.GetById;
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

        var response = new GetAllVehicleResponse(veiculos);
        var result = Result.Success(response);

        _mediatorMock.Setup(m => m.Send(
            It.IsAny<GetAllVehicleQuery>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(result);
        
        var actionResult = await _controller.GetAll();
        
        Assert.IsInstanceOfType(actionResult, typeof(Ok<GetAllVehicleResponse>));
        var okResult = actionResult as Ok<GetAllVehicleResponse>;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(response, okResult.Value);
    }

    [TestMethod]
    public async Task Shold_ReturnsNotFound_When_VehicleList_Are_Not_Valid()
    {
        var error = new Error("404", "Vehicle not found");
        var expectedResult = Result.Failure<GetAllVehicleResponse>(error);
        
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllVehicleQuery>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);
        
        var actionResult = await _controller.GetAll();
        Assert.IsInstanceOfType(actionResult, typeof(NotFound<Error>));
        var notFoundResult = actionResult as NotFound<Error>;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(error, notFoundResult.Value);
    }

    [TestMethod]
    public async Task Shold_ReturnsOk_When_Vehicle_Are_Valid()
    {
        var vehicle = new VeiculoDto { Placa = "ABC1234", Nome = "Uno" };
        var response = new GetVehicleByIdResponse(vehicle);
        var expectedResult = Result.Success(response);

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetVehicleByIdQuery>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);
        
        var actionResult = await _controller.GetById(vehicle.Id);
        Assert.IsInstanceOfType(actionResult, typeof(Ok<GetVehicleByIdResponse>));
        var okResult = actionResult as Ok<GetVehicleByIdResponse>;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(response, okResult.Value);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public async Task Shold_ReturnsNotFound_When_Vehicle_Are_Not_Valid()
    {
        var error = new Error("404", "Vehicle not found");
        var expectedResult = Result.Failure<GetVehicleByIdResponse>(error);
        
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetVehicleByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var actionResult = await _controller.GetById(2);
        Assert.IsInstanceOfType(actionResult, typeof(NotFound<Error>));
        var notFoundResult = actionResult as NotFound<Error>;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(error, notFoundResult.Value);
    }
}