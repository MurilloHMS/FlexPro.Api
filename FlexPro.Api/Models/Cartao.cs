using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexPro.Api.Models
{
    public class Cartao
    {
        public string? Nome { get; set; }
        public DateTime? Data { get; set; }
        public string? Descricao { get; set; }
        public decimal? Valor { get; set; }
        public string? Categoria { get; set; }
    }
}