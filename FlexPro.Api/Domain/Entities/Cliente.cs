namespace FlexPro.Api.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string CodigoSistema { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string EmailTeste { get; set; } = "murillo.henrique@proautokimium.com.br";
    }
}
