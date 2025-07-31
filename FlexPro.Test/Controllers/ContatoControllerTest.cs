using System.Net;
using FlexPro.Application.DTOs.Contato;
using FlexPro.Domain.Enums;
using FlexPro.Test.Setup;
using Xunit.Abstractions;

namespace FlexPro.Test.Controllers;

public class ContatoControllerTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _client;

    public ContatoControllerTest(CustomWebApplicationFactory factory, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient();
        TestAuthenticate.AuthenticateAsync(_client).GetAwaiter().GetResult();
    }

    [Fact]
    public async Task Post_Contato_Should_Return_Created()
    {
        var dto = new ContatoRequestDto
        {
            Nome = "Contato Teste",
            Email = "Teste@gmail.com",
            StatusContato = StatusContatoE.NaoContatado,
            TipoContato = TipoContatoE.DuvidaProduto,
            Mensagem = "Mensagem de teste",
            NomeEmpresa = "Empresa de teste"
        };
        var response = await _client.PostAsJsonAsync("api/contato", dto);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine($"Erros de Validação: {errorContent}");
        }

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Post_Contato_Should_Return_BadRequest_With_Email_Invalid()
    {
        var dto = new ContatoRequestDto
        {
            Nome = "Contato Teste",
            Email = "email invalido",
            StatusContato = StatusContatoE.NaoContatado,
            TipoContato = TipoContatoE.DuvidaProduto,
            Mensagem = "Mensagem de teste",
            NomeEmpresa = "Empresa de teste"
        };

        var response = await _client.PostAsJsonAsync("/api/contato", dto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}