using ClosedXML.Excel;
using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FlexPro.Api.Repository
{
    public class AbastecimentoRepository : IAbastecimentoRepository
    {
        private AppDbContext _context;
        public AbastecimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddFuelSupply(Abastecimento fuelSupply)
        {
            await _context.Abastecimento.AddAsync(fuelSupply);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeFuelSupply(IList<Abastecimento> fuelSupplies)
        {
            await _context.Abastecimento.AddRangeAsync(fuelSupplies);
            await _context.SaveChangesAsync();
        }

        public async Task<string> CalculateFuelSupply(IList<Abastecimento> fuelSupplyCurrentMonth, IList<Abastecimento> fuelSupplyLastMonth, string type)
        {
            var sb = new StringBuilder();
            try
            {
                var totalLitrosMesAtual = fuelSupplyCurrentMonth.Sum(a => a.Litros);
                var totalLitrosMesAnterior = fuelSupplyLastMonth.Sum(a => a.Litros);

                var totalPercorridoMesAtual = fuelSupplyCurrentMonth.Sum(a => a.DiferencaHodometro);
                var totalPercorridoMesAnterior = fuelSupplyLastMonth.Sum(a => a.DiferencaHodometro);

                var mediaKmMesAtual = fuelSupplyCurrentMonth.Average(a => a.MediaKm);
                var mediaKmMesAnterior = fuelSupplyLastMonth.Average(a => a.MediaKm);

                var valorTotalGastoMesAtual = fuelSupplyCurrentMonth.Sum(a => a.ValorTotalTransacao);
                var valorTotalGastoMesAnterior = fuelSupplyLastMonth.Sum(a => a.ValorTotalTransacao);

                var mediaPrecoLitroMesAtual = fuelSupplyCurrentMonth.Average(a => a.Preco);
                var mediaPrecoLitroMesAnterior = fuelSupplyLastMonth.Average(a => a.Preco);

                sb.AppendLine($"Abastecimento {type}");
                sb.AppendLine($"Quantidade de litros abastecidos: {CalcularDesempenho(totalLitrosMesAtual, totalLitrosMesAnterior, "N")}");
                sb.AppendLine($"Média de KM/L: {CalcularDesempenho(mediaKmMesAtual, mediaKmMesAnterior, "N")}");
                sb.AppendLine($"Média de Preço/L: {CalcularDesempenho(mediaPrecoLitroMesAtual, mediaPrecoLitroMesAnterior, "C")}");
                sb.AppendLine($"Valor Total Gasto: {CalcularDesempenho(valorTotalGastoMesAtual, valorTotalGastoMesAnterior, "C")}");
                sb.AppendLine($"Distancia percorrida: {CalcularDesempenho(totalPercorridoMesAtual, totalPercorridoMesAnterior, "N0")}");
                sb.AppendLine();
            }
            catch (Exception)
            {

            }

            return sb.ToString();
        }

        public async Task<string> CalculateGeneralFuelSupply(DateTime date)
        {
            var currentDate = date.ToUniversalTime();
            var currentMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1).ToUniversalTime();
            var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);
            var lastMonthStart = currentMonthStart.AddMonths(-1);
            var lastMonthEnd = currentMonthEnd.AddMonths(-1);

            var fuelSupplycurrentMonth = await _context.Abastecimento.Where(a => a.DataDoAbastecimento >= currentMonthStart && a.DataDoAbastecimento <= currentMonthEnd).ToListAsync();
            var fuelSupplyLastMonth = await _context.Abastecimento.Where(a => a.DataDoAbastecimento >= lastMonthStart && a.DataDoAbastecimento <=  lastMonthEnd).ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine(await CalculateFuelSupply(fuelSupplycurrentMonth, fuelSupplyLastMonth, "Geral"));

            return sb.ToString();
        }

        public async Task<string> CalculateIndividualFuelSupply(DateTime date)
        {
            var currentDate = date.ToUniversalTime();
            var currentMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1).ToUniversalTime();
            var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);
            var lastMonthStart = currentMonthStart.AddMonths(-1);
            var lastMonthEnd = currentMonthEnd.AddMonths(-1);

            var fuelSupplycurrentMonth = await _context.Abastecimento.Where(a => a.DataDoAbastecimento >= currentMonthStart && a.DataDoAbastecimento <= currentMonthEnd).ToListAsync();
            var fuelSupplyLastMonth = await _context.Abastecimento.Where(a => a.DataDoAbastecimento >= lastMonthStart && a.DataDoAbastecimento <= lastMonthEnd).ToListAsync();
            var employees = await _context.Abastecimento.Select(a => a.NomeDoMotorista).Distinct().ToListAsync();

            var sb = new StringBuilder();

            foreach (var funcionario in employees.OrderBy(a => a))
            {
                try
                {
                    var currentFuelSupplyByEmployee = fuelSupplycurrentMonth.Where(a => a.NomeDoMotorista == funcionario).ToList();
                    var lastFuelSupplyByEmploye = fuelSupplyLastMonth.Where(a => a.NomeDoMotorista == funcionario).ToList();

                    sb.AppendLine(await CalculateFuelSupply(currentFuelSupplyByEmployee, lastFuelSupplyByEmploye, $"{funcionario}"));
                }
                catch (Exception)
                {
                    continue;
                }

            }
            return sb.ToString();
        }

        public async Task<string> CalculateSetorFuelSupply(DateTime date)
        {
            var currentDate = date.ToUniversalTime();
            var currentMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1).ToUniversalTime();
            var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);
            var lastMonthStart = currentMonthStart.AddMonths(-1);
            var lastMonthEnd = currentMonthEnd.AddMonths(-1);

            var fuelSupplycurrentMonth = await _context.Abastecimento.Where(a => a.DataDoAbastecimento >= currentMonthStart && a.DataDoAbastecimento <= currentMonthEnd).ToListAsync();
            var fuelSupplyLastMonth = await _context.Abastecimento.Where(a => a.DataDoAbastecimento >= lastMonthStart && a.DataDoAbastecimento <= lastMonthEnd).ToListAsync();
            var departments = await _context.Abastecimento.Select(a => a.Departamento).Distinct().ToListAsync();

            var sb = new StringBuilder();

            foreach(var department in departments)
            {
                var currentFuelSupplyByDepartment = fuelSupplycurrentMonth.Where(a => a.Departamento == department).ToList();
                var lastFuelSupplyByDepartment = fuelSupplyLastMonth.Where(a => a.Departamento == department).ToList();

                sb.AppendLine(await CalculateFuelSupply(currentFuelSupplyByDepartment, lastFuelSupplyByDepartment, $"{department}"));
            }

            return sb.ToString();
        }

        public Task ExportData(IList<Abastecimento> fuelSupplies)
        {
            throw new NotImplementedException();
        }

        public Task ImportData()
        {
            throw new NotImplementedException();
        }

        public Task RemoveFuelSupply(int fuelSupplyId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Abastecimento>> GetFuelSupply()
        {
            var fuelSupply = await _context.Abastecimento.ToListAsync();
            return fuelSupply ?? Enumerable.Empty<Abastecimento>();
        }

        public async Task<IEnumerable<Abastecimento>> CollectFuelSupplyData(IFormFile file)
        {
            List<Abastecimento> fuelSupplies = new List<Abastecimento>();
            using (var stream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(stream);
                stream.Position = 0;

                using (XLWorkbook workbook = new(stream))
                {
                    var planilha = workbook.Worksheets.FirstOrDefault();
                    var fileData = planilha?.RowsUsed().Skip(1).Select(row => new Abastecimento
                    {
                        DataDoAbastecimento = row.Cell(1).TryGetValue<string>(out var dataAbastecimento)
                            ? DateTime.TryParse(dataAbastecimento, out var dataConvertida) ? dataConvertida.ToUniversalTime() : default
                            : default,
                        Uf = row.Cell(2).TryGetValue<string>(out var uf) ? uf : default,
                        Placa = row.Cell(3).TryGetValue<string>(out var placa) ? placa : default,
                        NomeDoMotorista = row.Cell(5).TryGetValue<string>(out var nomeDoMotorista) ? nomeDoMotorista : default,
                        HodometroAtual = row.Cell(6).TryGetValue<double>(out var hodometroAtual) ? hodometroAtual : default,
                        HodometroAnterior = row.Cell(7).TryGetValue<double>(out var hodometroAnterior) ? hodometroAnterior : default,
                        DiferencaHodometro = row.Cell(8).TryGetValue<double>(out var diferencaHodometro) ? diferencaHodometro : default,
                        MediaKm = row.Cell(9).TryGetValue<double>(out var mediaKm) ? mediaKm : default,
                        Combustivel = row.Cell(10).TryGetValue<string>(out var combustivel) ? combustivel : default,
                        Litros = row.Cell(11).TryGetValue<double>(out var litros) ? litros : default,
                        ValorTotalTransacao = row.Cell(12).TryGetValue<decimal>(out var valorTotalTransacao) ? valorTotalTransacao : default,
                        Preco = row.Cell(13).TryGetValue<decimal>(out var preco) ? preco : default
                    }).ToList();

                    fileData?.RemoveAll(a => a.Preco == 0);
                    fuelSupplies.AddRange(fileData ?? new List<Abastecimento>());
                }
            }

            return fuelSupplies;
        }

        public static string CalcularDesempenho<T>(T valorAtual, T valorAnterior, string tipo) where T : struct
        {
            if (valorAtual.Equals(0))
            {
                return "Sem Dados Para Comparação";
            }
            
            var porcentagem = CalcularPorcentagem(Convert.ToDouble(valorAtual), Convert.ToDouble(valorAnterior));
            var descricao = porcentagem < 0 ? "caiu" : "aumentou";
            string desempenho = default;
            switch (tipo.ToString())
            {
                case "C":
                    desempenho = $"{descricao} em {porcentagem:P2} de {valorAnterior:C} para {valorAtual:C}";
                    break;
                case "N0":
                    desempenho = $"{descricao} em {porcentagem:P2} de {valorAnterior:N0} para {valorAtual:N0}";
                    break;
                case "N":
                    desempenho = $"{descricao} em {porcentagem:P2} de {valorAnterior:N} para {valorAtual:N}";
                    break;
            }
            return desempenho;
        }

        public static double CalcularPorcentagem(double? atual, double? anterior)
        {
            if (anterior == 0) return 0;
            return ((atual.Value / anterior.Value) - 1);
        }
    }
}
