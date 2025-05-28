using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexPro.Api.Domain.Entities
{
    public abstract class Entidade
    {
        public int Id { get; set; }
		public string Nome { get; set; }
        public string? CodigoSistema { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; } =  true;
    }
}