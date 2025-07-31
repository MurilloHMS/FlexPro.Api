using FlexPro.Api.Application.Commands.Email;
using FlexPro.Api.Controllers;
using FlexPro.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlexPro.Test.Controllers;

public class EmailControllerTest
{
    private readonly EmailController _controller;
    private readonly Mock<IMediator> _mediatorMock;

    public EmailControllerTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new EmailController(_mediatorMock.Object);
    }

    [Fact]
    public async Task SendEmailAsync_ShouldReturnOkResult_WhenCommandSucceeds()
    {
        //Arrange
        var emailData = new Email
        {
            To = "test@example.com",
            Subject = "Test Subject",
            Body = "Test Body"
        };

        var expectedResult = new OkObjectResult("E-mail enviado com sucesso");

        _mediatorMock.Setup(m => m.Send(It.Is<SendEmailCommand>(cmd => cmd.EmailData == emailData), default))
            .ReturnsAsync(expectedResult);

        //act
        var result = await _controller.SendEmailAsync(emailData);

        //assert

        Assert.IsType<OkObjectResult>(result);
        var returnedResult = result as OkObjectResult;
        Assert.Equal("E-mail enviado com sucesso", returnedResult?.Value);
        Assert.Equal(expectedResult, returnedResult);
        Assert.Equal(200, returnedResult?.StatusCode);
        _mediatorMock.Verify(m => m.Send(It.IsAny<SendEmailCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendEmailAsync_ShouldReturnBadRequest_WhenCommandFails()
    {
        //Arrange
        var emailData = new Email
        {
            To = "test@example.com",
            Subject = "Test Subject",
            Body = "Test Body"
        };

        var expectedResult = new BadRequestObjectResult("Erro ao enviar e-mail");

        _mediatorMock.Setup(m => m.Send(It.Is<SendEmailCommand>(cmd => cmd.EmailData == emailData), default))
            .ReturnsAsync(expectedResult);

        //act
        var result = await _controller.SendEmailAsync(emailData);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
        var returnedResult = result as BadRequestObjectResult;
        Assert.Equal(expectedResult, result);
        Assert.Equal(400, returnedResult?.StatusCode);
        Assert.Equal("Erro ao enviar e-mail", returnedResult?.Value);

        _mediatorMock.Verify(m => m.Send(It.Is<SendEmailCommand>(cmd => cmd.EmailData == emailData), default),
            Times.Once);
    }

    [Fact]
    public async Task sendInformativoAsync_ShouldReturnOkResult_WhenCommandSucceeds()
    {
        //Arrange
        var informativos = new List<Informativo>
        {
            new()
            {
                CodigoCliente = "01", ClienteCadastrado = true, Data = DateTime.Now, EmailCliente = "teste@example.com",
                FaturamentoTotal = 1289.00m, MediaDiasAtendimento = 12, NomeDoCliente = "Teste Cliente 01",
                Mes = "Janeiro", ProdutoEmDestaque = "Kimi Ab200", QuantidadeDeLitros = 23, QuantidadeDeProdutos = 1443,
                QuantidadeNotasEmitidas = 12, Status = "Success", ValorDePeçasTrocadas = 12345.00m
            },
            new()
            {
                CodigoCliente = "02", ClienteCadastrado = true, Data = DateTime.Now, EmailCliente = "teste@example.com",
                FaturamentoTotal = 1289.00m, MediaDiasAtendimento = 12, NomeDoCliente = "Teste Cliente 01",
                Mes = "Janeiro", ProdutoEmDestaque = "Kimi Ab200", QuantidadeDeLitros = 23, QuantidadeDeProdutos = 1443,
                QuantidadeNotasEmitidas = 12, Status = "Success", ValorDePeçasTrocadas = 12345.00m
            },
            new()
            {
                CodigoCliente = "03", ClienteCadastrado = true, Data = DateTime.Now, EmailCliente = "teste@example.com",
                FaturamentoTotal = 1289.00m, MediaDiasAtendimento = 12, NomeDoCliente = "Teste Cliente 01",
                Mes = "Janeiro", ProdutoEmDestaque = "Kimi Ab200", QuantidadeDeLitros = 23, QuantidadeDeProdutos = 1443,
                QuantidadeNotasEmitidas = 12, Status = "Success", ValorDePeçasTrocadas = 12345.00m
            },
            new()
            {
                CodigoCliente = "04", ClienteCadastrado = true, Data = DateTime.Now, EmailCliente = "teste@example.com",
                FaturamentoTotal = 1289.00m, MediaDiasAtendimento = 12, NomeDoCliente = "Teste Cliente 01",
                Mes = "Janeiro", ProdutoEmDestaque = "Kimi Ab200", QuantidadeDeLitros = 23, QuantidadeDeProdutos = 1443,
                QuantidadeNotasEmitidas = 12, Status = "Success", ValorDePeçasTrocadas = 12345.00m
            }
        };

        var expectedResult = new OkObjectResult("Informativos enviados com sucesso");

        _mediatorMock.Setup(m =>
                m.Send(It.Is<SendInformativoCommand>(cmd => cmd.Informativos.SequenceEqual(informativos)), default))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.SendInformativosAsync(informativos);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.Equal("Informativos enviados com sucesso", okResult?.Value);
        Assert.Equal(200, okResult?.StatusCode);

        _mediatorMock.Verify(
            m => m.Send(It.Is<SendInformativoCommand>(cmd => cmd.Informativos.SequenceEqual(informativos)), default),
            Times.Once());
    }

    [Fact]
    public async Task sendInformativoAsync_ShouldReturnBadRequest_WhenCommandFails()
    {
        //Arrange
        var informativos = new List<Informativo>
        {
            new()
            {
                CodigoCliente = "01", ClienteCadastrado = true, Data = DateTime.Now, EmailCliente = "teste@example.com",
                FaturamentoTotal = 1289.00m, MediaDiasAtendimento = 12, NomeDoCliente = "Teste Cliente 01",
                Mes = "Janeiro", ProdutoEmDestaque = "Kimi Ab200", QuantidadeDeLitros = 23, QuantidadeDeProdutos = 1443,
                QuantidadeNotasEmitidas = 12, Status = "Success", ValorDePeçasTrocadas = 12345.00m
            },
            new()
            {
                CodigoCliente = "02", ClienteCadastrado = true, Data = DateTime.Now, EmailCliente = "teste@example.com",
                FaturamentoTotal = 1289.00m, MediaDiasAtendimento = 12, NomeDoCliente = "Teste Cliente 01",
                Mes = "Janeiro", ProdutoEmDestaque = "Kimi Ab200", QuantidadeDeLitros = 23, QuantidadeDeProdutos = 1443,
                QuantidadeNotasEmitidas = 12, Status = "Success", ValorDePeçasTrocadas = 12345.00m
            },
            new()
            {
                CodigoCliente = "03", ClienteCadastrado = true, Data = DateTime.Now, EmailCliente = "teste@example.com",
                FaturamentoTotal = 1289.00m, MediaDiasAtendimento = 12, NomeDoCliente = "Teste Cliente 01",
                Mes = "Janeiro", ProdutoEmDestaque = "Kimi Ab200", QuantidadeDeLitros = 23, QuantidadeDeProdutos = 1443,
                QuantidadeNotasEmitidas = 12, Status = "Success", ValorDePeçasTrocadas = 12345.00m
            },
            new()
            {
                CodigoCliente = "04", ClienteCadastrado = true, Data = DateTime.Now, EmailCliente = "teste@example.com",
                FaturamentoTotal = 1289.00m, MediaDiasAtendimento = 12, NomeDoCliente = "Teste Cliente 01",
                Mes = "Janeiro", ProdutoEmDestaque = "Kimi Ab200", QuantidadeDeLitros = 23, QuantidadeDeProdutos = 1443,
                QuantidadeNotasEmitidas = 12, Status = "Success", ValorDePeçasTrocadas = 12345.00m
            }
        };

        var expectedResult = new BadRequestObjectResult("Lista com dados está vazia");

        _mediatorMock
            .Setup(m => m.Send(It.Is<SendInformativoCommand>(cmd => cmd.Informativos.SequenceEqual(informativos)),
                default)).ReturnsAsync(expectedResult);

        //act
        var result = await _controller.SendInformativosAsync(informativos);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.Equal("Lista com dados está vazia", badRequestResult?.Value);
    }
}