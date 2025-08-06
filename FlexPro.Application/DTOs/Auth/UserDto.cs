namespace FlexPro.Application.DTOs.Auth;

public class UserDto
{
    public string Id { get; set; } = String.Empty;
    public string Username { get; set; } = String.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string>? Roles { get; set; }
}