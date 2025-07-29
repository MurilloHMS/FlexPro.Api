using ClosedXML.Excel;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Services;

public class InventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Byte[]> ExportInventoryToSheetAsync(DateTime? date = null)
    {
        try
        {
            if (date == null) return null;

            var mov = await _context.InventoryMovement.ToListAsync();
            var dayStock = mov
                .Where(m => m.Data.Equals(date))
                .Union(
                    mov.Where(m => m.Data < date)
                        .GroupBy(m => m.SystemId)
                        .Select(g => g.OrderByDescending(m => m.Data).First())
                )
                .GroupBy(m => m.SystemId)
                .Select(g => g.OrderByDescending(m => m.Data).First()).ToList();
            
            var products = await _context.Products.ToListAsync();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Movimentacoes");
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Nome";
                worksheet.Cell(1, 3).Value = "Data do ultimo estoque";
                worksheet.Cell(1, 4).Value = "Quantidade";

                for (int i = 0; i < dayStock.Count; i++)
                {
                    var movimentations = dayStock[i];
                    var product = products.FirstOrDefault(m => m.Id.Equals(movimentations.SystemId));
                    worksheet.Cell(i + 2, 1).Value = movimentations.SystemId;
                    worksheet.Cell(i + 2, 2).Value = product!.Nome;
                    worksheet.Cell(i + 2, 3).Value = movimentations.Data!.Value.ToString("dd/MM/yyyy");
                    worksheet.Cell(i + 2, 4).Value = movimentations.Quantity;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Products>> GetAllProductsWithLowStockAsync()
    {
        var query = await _context.Products.Select(p => new
            {
                Produto = p,
                Total = _context.InventoryMovement.Where(m => m.SystemId.Equals(p.Id)).Sum(m => m.Quantity)
            }).Where(p => p.Produto.MinimumStock.HasValue && p.Total <= p.Produto.MinimumStock.Value)
            .Select(p => p.Produto)
            .ToListAsync();
        return query;
    }
}