using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

[Table("inventory_products")]
public class Products : Entity
{
    [Column("system_code")]
    public string SystemCode { get; set; } = string.Empty;
    [Column("name")]
    public string Nome { get; set; } = string.Empty;
    [Column("minimum_stock")]
    public int? MinimumStock { get; set; }
}