using FlexPro.Api.Controllers;
using FlexPro.Application.DTOs.Auth;
using FlexPro.Application.UseCases.Auth;
using FlexPro.Application.UseCases.Users.Create;
using FlexPro.Application.UseCases.Users.UpdateUserRole;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlexPro.Api.Test.Controllers;

[TestClass]
public class AuthControllerTests
{
    private AuthController _authController = null!;
    private Mock<IMediator> _mediatorMock = null!;

    [TestInitialize]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _authController = new AuthController(_mediatorMock.Object);
    }

    [TestMethod]
    public async Task Login_ReturnOk_WithToken_WhenCredentialsAreValid()
    {
        var command = new LoginRequest { Username = "teste@exemplo.com", Password = "1234" };
        LoginResponse expectedToken ="fake-jwt-token" ;

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<AuthenticateLoginCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedToken);

        var result = await _authController.Login(command);

        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);

        var actualToken = okResult.Value as LoginResponse;
        Assert.IsNotNull(actualToken);
        Assert.AreEqual(expectedToken.Token, actualToken.Token);
    }

    [TestMethod]
    public async Task Login_ReturnsNotFound_WhenCredentialsAreInvalid()
    {
        var command = new LoginRequest{Username = "teste@exemplo.com", Password = "1234"};

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<LoginRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string?)null);

        var result = await _authController.Login(command);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(404, notFoundResult.StatusCode);
        Assert.AreEqual("Usuário ou senha incorretos", notFoundResult.Value);
    }

    [TestMethod]
    public async Task Register_ReturnsOk_WhenCredentialsAreValid()
    {
        var registerDto = new RegisterDto { Password = "1234", Roles = ["Departamento","Departamento"], Username = "teste@exemplo.com" };
        var expectedToken = "fake-jwt-token";

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedToken);

        var result = await _authController.Register(registerDto);
        
        Assert.IsTrue(result.GetType().IsGenericType && result.GetType().GetGenericTypeDefinition() == typeof(Ok<>));
        var valueProp = result.GetType().GetProperty("Value");
        Assert.IsNotNull(valueProp);

        var value = valueProp.GetValue(result);
        Assert.IsNotNull(value);

        var tokenProp = value.GetType().GetProperty("token");
        Assert.IsNotNull(tokenProp);

        var actualToken = tokenProp.GetValue(value) as string;
        Assert.AreEqual(expectedToken, actualToken);
    }

    
    [TestMethod]
    public async Task Register_ReturnsNotNull_WhenCredentialsAreInvalid()
    {
        var registerDto = new RegisterDto { Password = "1234",Roles = ["Departamento","Departamento"] };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string?)null);

        var result = await _authController.Register(registerDto);

        var notFoundResult = result as NotFound<string>;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(404, notFoundResult.StatusCode);
        Assert.AreEqual("Credenciais incorretas", notFoundResult.Value);
    }


    [TestMethod]
    public async Task Shold_Success_When_Role_is_include()
    {
        var role = new UpdateUserRoleDto { Role = "Departamento", Username = "test@example.com" };
        const string expectedResult = "Role adicionada com sucesso";

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserRoleCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _authController.AddRole(role);

        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(expectedResult, okResult.Value);
    }
}