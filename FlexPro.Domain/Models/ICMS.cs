namespace FlexPro.Domain.Models;

public class Icms
{
    public decimal VIcms { get; set; }
    public string? NNf { get; set; } = string.Empty;
    public decimal VPis { get; set; }
    public decimal VCofins { get; set; }
}