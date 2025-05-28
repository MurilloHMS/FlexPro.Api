using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexPro.Api.Domain.Entities
{
    public class Receita
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Embalagem { get; set; }
        public decimal ValorMaoDeObra { get; set; }
        public double Caixas { get; set; }
        
        public virtual ICollection<ReceitaMateriaPrima>  ReceitaMateriaPrima { get; set; }
    }
}