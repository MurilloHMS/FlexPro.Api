using Microsoft.AspNetCore.Identity;

namespace FlexPro.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Departamento { get; set; }
}