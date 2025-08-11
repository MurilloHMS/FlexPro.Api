namespace FlexPro.Domain.Interfaces.Pdf;

public interface IFileNameSanitizer
{
    string Sanitize(string fileName);
}