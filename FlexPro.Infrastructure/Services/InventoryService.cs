using ClosedXML.Excel;
using FlexPro.Domain.Entities;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Services;

public class InventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]?> ExportInventoryToSheetAsync(DateTime? date = null)
    {
        try
        {
            if (date == null) return null;

            var dayStock = await _context.InventoryMovement
                .Where(m => m.Data <= date)
                .GroupBy(m => m.InventoryProductId)
                .Select(g => g.OrderByDescending(m => m.Data).First())
                .Include(m => m.InventoryProduct)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Movimentações");
            
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Nome";
            worksheet.Cell(1, 3).Value = "Data do ultimo estoque";
            worksheet.Cell(1, 4).Value = "Quantidade";

            for (int i = 0; i < dayStock.Count; i++)
            {
                var mov = dayStock[i];
                worksheet.Cell(i + 2, 1).Value = mov.InventoryProductId;
                worksheet.Cell(i + 2, 2).Value = mov.InventoryProduct?.Name;
                worksheet.Cell(i + 2, 3).Value = mov.Data?.ToString("dd/MM/yyyy");
                worksheet.Cell(i + 2, 4).Value = mov.Quantity;
            }
            
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<InventoryProducts>> GetAllProductsWithLowStockAsync()
    {
        return await _context.InventoryProducts
            .Where(p => p.MinimumStock.HasValue &&
                        _context.InventoryMovement
                            .Where(m => m.InventoryProductId == p.Id)
                            .Sum(m => m.Quantity) <= p.MinimumStock.Value)
            .ToListAsync();
    }
}