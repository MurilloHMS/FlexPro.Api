using Microsoft.AspNetCore.Identity;

namespace FlexPro.Api.Infrastructure.Persistance
{
    public class ApplicationUser : IdentityUser 
    {
        public string? Departamento { get; set; }
    }
}
