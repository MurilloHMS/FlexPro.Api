using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities
{
    public class Cliente : Entidade
    {
        // Cliente Vindo do site
        public StatusContato_e Status { get; set; }
        public string Contato { get; set; } = string.Empty;
        public FormasDeContato_e MeioDeContato { get; set; }
    }
}
