using ClosedXML.Excel;
using FlexPro.Api.Domain.Entities;
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
                                Data = row.Cell(2).TryGetValue<DateTime>(out var data) ? data : default,
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

        public async Task<IEnumerable<Informativo>> CreateInfoData(IEnumerable<InformativoNFe> nfeInfo, IEnumerable<InformativoOS> osInfo, IEnumerable<InformativoPecasTrocadas> pecasInfo)
        {
            try
            {
                List<Informativo> informativos = new List<Informativo>();

                List<Cliente> clientes = new List<Cliente>();

                clientes = await _context.Cliente.ToListAsync();

                if (clientes.Any() && nfeInfo.Any() && osInfo.Any() && pecasInfo.Any())
                {
                    foreach (var cliente in clientes)
                    {
                        var clienteNfeInfo = nfeInfo.Where(nfe => nfe.CodigoCliente == cliente.CodigoSistema);
                        var clienteOsInfo = osInfo.Where(os => os.CodigoCliente == cliente.CodigoSistema);
                        var clientePecas = pecasInfo.Where(pecas => pecas.CodigoCliente == cliente.CodigoSistema);

                        var informativo = new Informativo
                        {
                            CodigoCliente = cliente.CodigoSistema,
                            NomeDoCliente = cliente.Nome,
                            Data = clienteNfeInfo.Select(d => d.Data.Date).FirstOrDefault(),
                            Mes = clienteNfeInfo.Select(d => d.Data.Date.ToString("MMMM")).FirstOrDefault(),
                            QuantidadeDeProdutos = clienteNfeInfo.Count(),
                            QuantidadeDeLitros = clienteNfeInfo.Sum(x => x.Quantidade),
                            QuantidadeNotasEmitidas = clienteNfeInfo.Select(x => x.NumeroNFe).Distinct().Count(),
                            MediaDiasAtendimento = (int)(clienteOsInfo.Any() ? clienteOsInfo.Sum(x => x.DiasDaSemana) : 0),
                            ProdutoEmDestaque = clienteNfeInfo.GroupBy(nfe => nfe.NomeDoProduto).OrderByDescending(group => group.Count()).FirstOrDefault()?.Key,
                            FaturamentoTotal = clienteNfeInfo.Sum(nfe => nfe.ValorTotalComImpostos),
                            ValorDePeçasTrocadas = clientePecas.Sum(pecas => pecas.CustoTotal),
                            ClienteCadastrado = true,
                            EmailCliente = cliente.Email
                        };

                        informativos.Add(informativo);
                    }
                }

                return informativos ?? new List<Informativo>();
            }
            catch (Exception)
            {
                return new List<Informativo>();
            }
        }
        
    }
}
