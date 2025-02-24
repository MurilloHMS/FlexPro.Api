using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexPro.Api.Models
{
    public class BoaSolucao
    {
        public string CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorTotal { get; set; }
        public bool? isValid { get; set; }
    }
}