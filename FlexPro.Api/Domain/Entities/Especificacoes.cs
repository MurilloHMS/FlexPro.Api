namespace FlexPro.Api.Domain.Entities;

public class Especificacoes
{
    public int Id { get; set; }
    public string Processador {get; set;}
    public int MemoriaRAM {get; set;}
    public int Armazenamento {get; set;}
    public string TipoArmazenamento {get; set;}
    
    public virtual Computador Computador  { get; set; }
    public int IdComputador  { get; set; }
}