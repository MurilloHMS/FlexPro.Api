using System.Net;
using FlexPro.Application.DTOs.Cliente;
using FlexPro.Domain.Enums;
using FlexPro.Test.Setup;
using Xunit.Abstractions;

namespace FlexPro.Test.Controllers;

public class ClienteControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _client;

    public ClienteControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient();
        TestAuthenticate.AuthenticateAsync(_client).GetAwaiter().GetResult();
    }

    [Fact]
    public async Task Post_Cliente_Should_Return_Created()
    {
        // Arrange
        var dto = new ClienteRequestDto
        {
            Nome = "Cliente Teste",
            Email = "teste@cliente.com",
            CodigoSistema = "SYS123",
            Status = StatusContatoE.NaoContatado,
            Contato = "emaildecontato@cliente.com",
            MeioDeContato = FormasDeContatoE.Email
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/cliente", dto);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine($"Erros de Validação: {errorContent}");
        }

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Post_Cliente_Should_Return_BadRequest_With_Email_Invalid()
    {
        var dto = new ClienteRequestDto
        {
            Nome = "Cliente teste",
            Email = "Emailinvalido.com",
            CodigoSistema = "SYS123",
            Status = StatusContatoE.NaoContatado,
            Contato = "emaildecontato@cliente.com",
            MeioDeContato = FormasDeContatoE.Email
        };

        //act
        var response = await _client.PostAsJsonAsync("/api/cliente", dto);

        //assert
        var errorContent = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(errorContent);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}