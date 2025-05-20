using System.Net;
using FlexPro.Api.Application.DTOs.Contato;
using FlexPro.Api.Domain;
using FlexPro.Test.Setup;

namespace FlexPro.Test.Controllers;

public class ContatoControllerTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private ContatoRequestDTO dto;

    public ContatoControllerTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        TestAuthenticate.AuthenticateAsync(_client).GetAwaiter().GetResult();
        dto = new ContatoRequestDTO()
        {
            Nome = "Contato Teste",
            Email = "Teste@gmail.com",
            StatusContato = StatusContato_e.AguardandoContato,
            TipoContato = TipoContato_e.DuvidaProduto,
            Mensagem = "Mensagem de teste",
            NomeEmpresa = "Empresa de teste"
        };
    }

    [Fact]
    public async Task Post_Contato_Should_Return_Created()
    {
        var response = await _client.PostAsJsonAsync("api/contato", dto);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Erros de Validação: {errorContent}");
        }

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Post_Contato_Should_Return_BadRequest_With_Email_Invalid()
    {
        dto.Email = "email invalido";
        var response = await _client.PostAsJsonAsync("/api/contato", dto);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}