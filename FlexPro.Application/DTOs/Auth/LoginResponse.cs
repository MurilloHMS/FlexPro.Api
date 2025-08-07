namespace FlexPro.Application.DTOs.Auth;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    
    public static implicit operator string(LoginResponse loginResponse) =>  loginResponse.Token;
    public static implicit operator LoginResponse(string token) =>  new LoginResponse { Token = token };
}