namespace FlexPro.Api.Application.DTOs.Cliente;

public class ClienteRequestDTO
{
    public string CodigoSistema { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public string EmailTeste { get; set; } = "murillo.henrique@proautokimium.com.br";
}