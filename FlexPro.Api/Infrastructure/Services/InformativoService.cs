using System.Globalization;
using ClosedXML.Excel;
using FlexPro.Api.Domain.Models;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace FlexPro.Api.Infrastructure.Services
{
    public class InformativoService
    {
        private readonly AppDbContext _context;

        public InformativoService(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<InformativoNFe>> ReadNfeData(IFormFile file)
        {
            try
            {
                List<InformativoNFe> dados = new();

                using (var stream = new MemoryStream())
                {
                    await file.OpenReadStream().CopyToAsync(stream);
                    stream.Position = 0;

                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheets.First();
                        var fileData = worksheet.RowsUsed()
                            .Skip(3)
                            .Select(row => new InformativoNFe
                            {
                                NumeroNFe = row.Cell(1).TryGetValue<int>(out var numeroNfe) ? numeroNfe : default,
                                Data = row.Cell(2).TryGetValue<string>(out var dataString) && 
                                       DateTime.TryParseExact(dataString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dataFormatada)
                                    ? dataFormatada : default,
                                CodigoCliente = row.Cell(3).TryGetValue<string>(out var codigoCliente) ? codigoCliente : default,
                                NomeDoCliente = row.Cell(4).TryGetValue<string>(out var nomeCliente) ? nomeCliente : default,
                                CodigoProduto = row.Cell(6).TryGetValue<string>(out var CodigoProduto) ? CodigoProduto : default,
                                TipoDeUnidade = row.Cell(7).TryGetValue<char>(out var tipoDeUnidade) ? tipoDeUnidade : default,
                                NomeDoProduto = row.Cell(8).TryGetValue<string>(out var nomeProduto) ? nomeProduto : default,
                                Quantidade = row.Cell(9).TryGetValue<double>(out var quantidade) ? quantidade : default,
                                ValorTotalComImpostos = row.Cell(10).TryGetValue<decimal>(out var valorTotal) ? valorTotal : default
                            }).ToList();

                        dados.AddRange(fileData);
                    }
                }

                return dados ?? new List<InformativoNFe>();

            }catch(Exception ex)
            {
                return new List<InformativoNFe>();
            }
        }

        public async Task<IEnumerable<InformativoOS>> ReadOsData(IFormFile file)
        {
            try
            {
                List<InformativoOS> dados = new();

                using (var stream = new MemoryStream())
                {
                    await file.OpenReadStream().CopyToAsync(stream);
                    stream.Position = 0;

                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheets.First();
                        var fileData = worksheet.RowsUsed()
                            .Skip(1)
                            .Select(row => new InformativoOS
                            {
                                NumOs = row.Cell(1).TryGetValue<int>(out var numeroOs) ? numeroOs : default,
                                CodigoCliente = row.Cell(2).TryGetValue<string>(out var codigoCliente) ? codigoCliente : default,
                                DataDeAbertura = row.Cell(4).TryGetValue<DateTime>(out var dataAbertura) ? dataAbertura : default,
                                DataDeFechamento = row.Cell(5).TryGetValue<DateTime>(out var dataFechamento) ? dataFechamento : default,
                                DiasDaSemana = row.Cell(6).TryGetValue<int>(out var diasSemana) ? diasSemana : default
                            }).ToList();

                        dados.AddRange(fileData);
                    }
                }

                return dados ?? new List<InformativoOS>();
            }
            catch (Exception)
            {
                return new List<InformativoOS>();
            }
        }

        public async Task<IEnumerable<InformativoPecasTrocadas>> ReadPecasTrocadasData(IFormFile file)
        {
            try
            {
                List<InformativoPecasTrocadas> dados = new();

                using (var stream = new MemoryStream())
                {
                    await file.OpenReadStream().CopyToAsync(stream);
                    stream.Position = 0;

                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheets.First();
                        var fileData = worksheet.RowsUsed()
                            .Skip(3)
                            .Select(row => new InformativoPecasTrocadas
                            {
                                CodigoCliente = row.Cell(1).TryGetValue<string>(out var codigoCliente) ? codigoCliente : default,
                                CustoTotal = row.Cell(3).TryGetValue<decimal>(out var custoTotal) ? custoTotal : default
                            }).ToList();

                        dados.AddRange(fileData);
                    }
                }

                return dados ?? new List<InformativoPecasTrocadas>();
            }
            catch (Exception)
            {
                return new List<InformativoPecasTrocadas>();
            }
        }

        public async Task<IEnumerable<Informativo>> CreateInfoData(
            IEnumerable<InformativoNFe> nfeInfo,
            IEnumerable<InformativoOS> osInfo,
            IEnumerable<InformativoPecasTrocadas> pecasInfo,
            string? month = null)
        {
            try
            {
                if (!nfeInfo.Any() || !osInfo.Any() || !pecasInfo.Any())
                    return Enumerable.Empty<Informativo>();

                var parceiros = await _context.Parceiro.ToListAsync();
                if (!parceiros.Any())
                    return Enumerable.Empty<Informativo>();

                var nfeGrouped = nfeInfo.GroupBy(x => x.CodigoCliente).ToDictionary(g => g.Key, g => g.ToList());
                var osGrouped = osInfo.GroupBy(x => x.CodigoCliente).ToDictionary(g => g.Key, g => g.ToList());
                var pecasGrouped = pecasInfo.GroupBy(x => x.CodigoCliente).ToDictionary(g => g.Key, g => g.ToList());

                var informativos = new List<Informativo>();

                foreach (var cliente in parceiros)
                {
                    nfeGrouped.TryGetValue(cliente.CodigoSistema, out var clienteNfeInfo);
                    osGrouped.TryGetValue(cliente.CodigoSistema, out var clienteOsInfo);
                    pecasGrouped.TryGetValue(cliente.CodigoSistema, out var clientePecasInfo);

                    if (clienteNfeInfo == null || !clienteNfeInfo.Any())
                        continue;

                    var data = clienteNfeInfo.First().Data;
                    var produtoEmDestaque = clienteNfeInfo
                        .GroupBy(nfe => nfe.NomeDoProduto)
                        .OrderByDescending(g => g.Count())
                        .FirstOrDefault()?.Key;

                    if (string.IsNullOrEmpty(produtoEmDestaque))
                        continue;

                    var informativo = new Informativo
                    {
                        CodigoCliente = cliente.CodigoSistema,
                        NomeDoCliente = cliente.Nome,
                        Data = data,
                        Mes = month ?? data.ToString("MMMM", CultureInfo.InvariantCulture),
                        QuantidadeDeProdutos = clienteNfeInfo.Count,
                        QuantidadeDeLitros = clienteNfeInfo.Sum(x => x.Quantidade),
                        QuantidadeNotasEmitidas = clienteNfeInfo.Select(x => x.NumeroNFe).Distinct().Count(),
                        QuantidadeDeVisitas = clienteOsInfo?.Count ?? 0,
                        MediaDiasAtendimento = clienteOsInfo != null && clienteOsInfo.Any() ? (int)clienteOsInfo.Sum(x => x.DiasDaSemana) : 0,
                        ProdutoEmDestaque = produtoEmDestaque,
                        FaturamentoTotal = clienteNfeInfo.Sum(x => x.ValorTotalComImpostos),
                        ValorDePeçasTrocadas = clientePecasInfo?.Sum(x => x.CustoTotal) ?? 0,
                        ClienteCadastrado = true,
                        EmailCliente = cliente.Email
                    };

                    informativos.Add(informativo);
                }

                return informativos;
            }
            catch (Exception)
            {
                return Enumerable.Empty<Informativo>();
            }
        }

    }
}
