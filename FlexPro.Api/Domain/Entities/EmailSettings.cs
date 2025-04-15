using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexPro.Api.Domain.Entities
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
    }
}