﻿using ClosedXML.Excel;
using FlexPro.Api.Data;
using FlexPro.Api.Models;
using FlexPro.Api.Repository;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FlexPro.Api.Services
{
    public class AbastecimentoService
    {
        private readonly AbastecimentoRepository _repository;
        private readonly AppDbContext _context;

        public AbastecimentoService(AppDbContext context)
        {
            _context = context;
            _repository = new AbastecimentoRepository(_context);
        }

        public async Task<string> CalcularAbastecimentoGeral(DateTime data)
        {
            var dataAtual = data.ToUniversalTime();
            var inicioMesAtual = new DateTime(dataAtual.Year, dataAtual.Month, 1).ToUniversalTime();
            var fimMesAtual = inicioMesAtual.AddMonths(1).AddDays(-1);
            var inicioMesAnterior = inicioMesAtual.AddMonths(-1);
            var fimMesAnterior = inicioMesAtual.AddDays(-1);

            var abastecimentoMesAtual = await _repository.GetFuelSupplyByDate(inicioMesAtual, fimMesAtual);
            var abastecimentoMesAnterior = await _repository.GetFuelSupplyByDate(inicioMesAnterior, fimMesAnterior);

            var sb = new StringBuilder();

            sb.AppendLine(await CalculaAbastecimento(abastecimentoMesAtual, abastecimentoMesAnterior, "Geral"));

            return sb.ToString();
        }
        public async Task<string> CalcularAbastecimentoSetor(DateTime data)
        {
            var dataAtual = data.ToUniversalTime();
            var inicioMesAtual = new DateTime(dataAtual.Year, dataAtual.Month, 1).ToUniversalTime();
            var fimMesAtual = inicioMesAtual.AddMonths(1).AddDays(-1);
            var inicioMesAnterior = inicioMesAtual.AddMonths(-1);
            var fimMesAnterior = inicioMesAtual.AddDays(-1);

            var abastecimentoMesAtual = await _repository.GetFuelSupplyByDate(inicioMesAtual, fimMesAtual);
            var abastecimentoMesAnterior = await _repository.GetFuelSupplyByDate(inicioMesAnterior, fimMesAnterior);
            var departamentos = await _context.Abastecimento.Select(a => a.Departamento).Distinct().ToListAsync();

            var sb = new StringBuilder();

            foreach (var departamento in departamentos)
            {
                var abastecimentoAtualDepto = abastecimentoMesAtual.Where(a => a.Departamento == departamento).ToList();
                var abastecimentoAnteriorDepto = abastecimentoMesAnterior.Where(a => a.Departamento == departamento).ToList();

                sb.AppendLine(await CalculaAbastecimento(abastecimentoAtualDepto, abastecimentoAnteriorDepto, $"{departamento}"));
            }
            return sb.ToString();
        }

        public async Task<string> CalcularAbastecimentoIndividual(DateTime data)
        {
            var dataAtual = data.ToUniversalTime();
            var inicioMesAtual = new DateTime(dataAtual.Year, dataAtual.Month, 1).ToUniversalTime();
            var fimMesAtual = inicioMesAtual.AddMonths(1).AddDays(-1);
            var inicioMesAnterior = inicioMesAtual.AddMonths(-1);
            var fimMesAnterior = inicioMesAtual.AddDays(-1);

            var abastecimentoMesAtual = await _repository.GetFuelSupplyByDate(inicioMesAtual, fimMesAtual);
            var abastecimentoMesAnterior = await _repository.GetFuelSupplyByDate(inicioMesAnterior, fimMesAnterior);
            var funcionarios = await _context.Abastecimento.Select(a => a.NomeDoMotorista).Distinct().ToListAsync();

            var sb = new StringBuilder();

            foreach (var funcionario in funcionarios.OrderBy(a => a))
            {
                try
                {
                    var abastecimentoAtualFuncionario = abastecimentoMesAtual.Where(a => a.NomeDoMotorista == funcionario).ToList();
                    var abastecimentoAnteriorFuncionario = abastecimentoMesAnterior.Where(a => a.NomeDoMotorista == funcionario).ToList();

                    sb.AppendLine(await CalculaAbastecimento(abastecimentoAtualFuncionario, abastecimentoAnteriorFuncionario, $"{funcionario}"));
                }
                catch (Exception)
                {
                    continue;
                }

            }
            return sb.ToString();
        }

        public async Task<string> CalculaAbastecimento(List<Abastecimento>? abastecimentoMesAtual, List<Abastecimento>? abastecimentoMesAnterior, string tipo)
        {
            var sb = new StringBuilder();
            try
            {
                var totalLitrosMesAtual = abastecimentoMesAtual.Sum(a => a.Litros);
                var totalLitrosMesAnterior = abastecimentoMesAnterior.Sum(a => a.Litros);

                var totalPercorridoMesAtual = abastecimentoMesAtual.Sum(a => a.DiferencaHodometro);
                var totalPercorridoMesAnterior = abastecimentoMesAnterior.Sum(a => a.DiferencaHodometro);

                var mediaKmMesAtual = abastecimentoMesAtual.Average(a => a.MediaKm);
                var mediaKmMesAnterior = abastecimentoMesAnterior.Average(a => a.MediaKm);

                var valorTotalGastoMesAtual = abastecimentoMesAtual.Sum(a => a.ValorTotalTransacao);
                var valorTotalGastoMesAnterior = abastecimentoMesAnterior.Sum(a => a.ValorTotalTransacao);

                var mediaPrecoLitroMesAtual = abastecimentoMesAtual.Average(a => a.Preco);
                var mediaPrecoLitroMesAnterior = abastecimentoMesAnterior.Average(a => a.Preco);

                sb.AppendLine($"Abastecimento {tipo}");
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

        public async Task<List<Abastecimento>> ColetarDadosAbastecimento(IFormFile arquivo)
        {
            List<Abastecimento> abastecimentos = new();

            using (var stream = new MemoryStream())
            {
                await arquivo.OpenReadStream().CopyToAsync(stream);
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
                    abastecimentos.AddRange(fileData ?? new List<Abastecimento>());
                }
            }

            return abastecimentos;
        }
    }
}
