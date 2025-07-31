using System.Net;
using System.Net.Http.Json;
using System.Text;
using FlexPro.Application.DTOs.Cliente;
using FlexPro.Domain.Enums;
using FlexPro.Test.Setup;
using Newtonsoft.Json;

namespace FlexPro.Test.Controllers;

public class ClienteControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ClienteControllerTests(CustomWebApplicationFactory factory)
    {
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
            Status = StatusContato_e.NaoContatado,
            Contato = "emaildecontato@cliente.com",
            MeioDeContato = FormasDeContato_e.Email
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/cliente", dto);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Erros de Validação: {errorContent}");
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
            Status = StatusContato_e.NaoContatado,
            Contato = "emaildecontato@cliente.com",
            MeioDeContato = FormasDeContato_e.Email
        };
        
        //act
        var response = await _client.PostAsJsonAsync("/api/cliente", dto);
        
        //assert
        var errorContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(errorContent);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    
}
