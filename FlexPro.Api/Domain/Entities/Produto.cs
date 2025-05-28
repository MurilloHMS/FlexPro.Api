using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexPro.Api.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoSistema { get; set; }
        public string Nome { get; set; }
    }
}