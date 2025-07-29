using System.ComponentModel.DataAnnotations;
using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities
{
    public class Categoria : Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
    }
}