using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Domain.Entities;

[Table("produtos")]
public class Products
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Key]
    [Column("system_code")]
    public string SystemCode { get; set; } = string.Empty;
    [Column("name")]
    public string Nome { get; set; } = string.Empty;
    [Column("minimum_stock")]
    public int? MinimumStock { get; set; }
}