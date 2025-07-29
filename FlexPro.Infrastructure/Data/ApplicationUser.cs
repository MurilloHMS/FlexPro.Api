using Microsoft.AspNetCore.Identity;

namespace FlexPro.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser 
    {
        public string? Departamento { get; set; }
    }
}
