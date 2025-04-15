using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexPro.Api.Domain.Entities
{
    public class Funcionario
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public string? Departamento { get; set; }
        public string? Hash { get; set; }
        public string? Gerente { get; set; }
        public string? Email { get; set; }
        public string? Hierarquia  { get; set; }
    }
}