using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities
{
    public abstract class Entidade : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string? CodigoSistema { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; } =  true;
    }
}