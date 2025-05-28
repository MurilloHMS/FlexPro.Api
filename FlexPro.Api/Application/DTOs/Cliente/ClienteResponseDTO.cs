using FlexPro.Api.Domain;

namespace FlexPro.Api.Application.DTOs.Cliente;

public class ClienteResponseDTO
{
    public int Id { get; set; }
    public string CodigoSistema { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public bool Ativo { get; set; }
    public StatusContato_e Status { get; set; }
    public string Contato { get; set; }
    public FormasDeContato_e MeioDeContato { get; set; }
}