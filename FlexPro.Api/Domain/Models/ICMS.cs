using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexPro.Api.Domain.Models
{
    public class ICMS
    {
        public decimal vICMS { get; set; }
        public string  nNF { get; set; }
        public decimal vPis { get; set; }
        public decimal vCofins { get; set; }
    }
}