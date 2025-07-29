namespace FlexPro.Domain.Entities;

public class Especificacoes
{
    public int Id { get; set; }
    public string Processador { get; set; } = string.Empty;
    public int MemoriaRam {get; set;}
    public int Armazenamento {get; set;}
    public string TipoArmazenamento { get; set; } = string.Empty;
    
    public virtual Computador? Computador  { get; set; }
    public int IdComputador  { get; set; }
}