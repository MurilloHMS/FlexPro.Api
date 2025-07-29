namespace FlexPro.Domain.Entities;

public class Parceiro : Entidade
{
    // Cliente Proauto Kimium (Recebe informativo)
    public string RazaoSocial { get; set; } = string.Empty;
    public string EmailTeste { get; set; } = "murillo.henrique@proautokimium.com.br";
    public bool RecebeEmail { get; set; } = true;
}