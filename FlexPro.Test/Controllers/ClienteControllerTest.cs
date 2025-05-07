using System.Net;
using System.Net.Http.Json;
using System.Text;
using FlexPro.Api.Application.DTOs.Auth;
using FlexPro.Api.Application.DTOs.Cliente;
using FlexPro.Test.Setup;
using Newtonsoft.Json;

namespace FlexPro.Test.Controllers;

public class ClienteControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ClienteControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        AuthenticateAsync().GetAwaiter().GetResult();
    }
    
    private async Task AuthenticateAsync()
    {
        var loginPayload = new LoginRequest
        {
            Username = "murillo.henrique@proautokimium.com.br",
            Password = "Xj7hpmtmma@"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/login", loginPayload);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<LoginResponse>();
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", json.Token);
    }

    [Fact]
    public async Task Post_Cliente_Should_Return_Created()
    {
        // Arrange
        var dto = new ClienteRequestDTO
        {
            Nome = "Cliente Teste",
            Email = "teste@cliente.com",
            CodigoSistema = "SYS123",
            Status = "on"
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
}
