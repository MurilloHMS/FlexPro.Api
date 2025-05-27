namespace FlexPro.Api.Domain.Entities
{
    public class Cliente : Entidade
    {
        // Cliente Vindo do site
        public StatusContato_e Status { get; set; }
        public string Contato { get; set; }
        public FormasDeContato_e MeioDeContato { get; set; }
    }
}
