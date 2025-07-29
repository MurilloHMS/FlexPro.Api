namespace FlexPro.Domain.Abstractions;

public record Error(string Code, string Message)
{
    public static Error None = new Error(string.Empty, string.Empty);
    public static Error NullValue = new Error("Error.NullValue", "Um valor nulo foi fornecido");
    
}