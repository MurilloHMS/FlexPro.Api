namespace FlexPro.Api.Application.DTOs.Auth
{
    public class RegisterDTO
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = "User";
    }
}
