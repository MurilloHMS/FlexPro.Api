using FlexPro.Api.Domain;

namespace FlexPro.Api.Application.DTOs.Contato;

public class ContatoResponseDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public TipoContato_e TipoContato { get; set; }
    public string? outro { get; set; }
    public string Mensagem { get; set; }
    public string NomeEmpresa { get; set; }
    public StatusContato_e StatusContato { get; set; }
    public DateTime DataSolicitadoContato { get; set; }
}