using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FlexPro.Api.Domain.Models
{
    public class Email
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string>? Cc { get; set; }
        public List<string>? Bcc { get; set; }
    }
}
