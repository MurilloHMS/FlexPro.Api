using FlexPro.Api.Application.Commands.Auth;
using FlexPro.Api.Application.DTOs.Auth;
using FlexPro.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlexPro.Api.Test.Controllers;

[TestClass]
public class AuthControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private AuthController _authController = null!;

    [TestInitialize]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _authController = new AuthController(_mediatorMock.Object);
    }

    [TestMethod]
    public async Task Login_ReturnOk_WithToken_WhenCredentialsAreValid()
    {
        var command = new LoginCommand("teste@exemplo.com", "1234" );
        var expectedToken = "fake-jwt-token";
        
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedToken);
        
        var result = await _authController.Login(command);

        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);

        var tokenProperty = okResult.Value!.GetType().GetProperty("token");
        Assert.IsNotNull(tokenProperty);
        
        var actualToken = tokenProperty.GetValue(okResult.Value!) as string;
        Assert.AreEqual(expectedToken, actualToken);
    }

    [TestMethod]
    public async Task Login_ReturnsNotFound_WhenCredentialsAreInvalid()
    {
        var command = new LoginCommand("teste@exemplo.com", "1234" );

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))!
            .ReturnsAsync((string?)null);
        
        var result = await _authController.Login(command);
        
        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(404, notFoundResult.StatusCode);
        Assert.AreEqual("Usuário ou senha incorretos",  notFoundResult.Value);
    }

    [TestMethod]
    public async Task Register_ReturnsOk_WhenCredentialsAreValid()
    {
        var registerDto = new RegisterDTO { Password = "1234", Role = "Departamento", Username = "teste@exemplo.com" };
        var expectedToken = "fake-jwt-token";
        
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedToken);
        
        var result = await _authController.Register(registerDto);
        
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        var tokenProperty = okResult.Value!.GetType().GetProperty("token");
        Assert.IsNotNull(tokenProperty);
        var actualToken = tokenProperty.GetValue(okResult.Value!) as string;
        Assert.AreEqual(expectedToken, actualToken);
        
    }

    [TestMethod]
    public async Task Register_ReturnsNotNull_WhenCredentialsAreInvalid()
    {
        var registerDto = new RegisterDTO { Password = "1234", Role = "Departamento" };
        
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))!
            .ReturnsAsync((string?)null);
        
        var result = await _authController.Register(registerDto);
        
        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(404, notFoundResult.StatusCode);
        Assert.AreEqual("Credenciais incorretas",  notFoundResult.Value);
    }

    [TestMethod]
    public async Task Shold_Success_When_Role_is_include()
    {
        var role = new UpdateUserRoleDTO(){Role = "Departamento", Username = "test@example.com"};
        string expectedResult = "Role adicionada com sucesso";
        
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserRoleCommand>(), It.IsAny<CancellationToken>()))!
            .ReturnsAsync(true);

        var result = await _authController.AddRole(role);
        
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(expectedResult, okResult.Value);

    }
}