using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities
{
    public class Funcionario : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string? Departamento { get; set; }
        public string? Hash { get; set; }
        public string? Gerente { get; set; }
        public string? Email { get; set; }
        public string? Hierarquia  { get; set; }
    }
}