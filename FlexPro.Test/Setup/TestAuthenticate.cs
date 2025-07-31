using System.Net.Http.Headers;
using System.Net.Http.Json;
using FlexPro.Application.DTOs.Auth;

namespace FlexPro.Test.Setup;

public static class TestAuthenticate
{
    public static async Task AuthenticateAsync(HttpClient _client)
    {
        var loginPayload = new LoginRequest
        {
            Username = "murillo.henrique@proautokimium.com.br",
            Password = "Xj7hpmtmma@"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/login", loginPayload);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<LoginResponse>();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", json.Token);
    }
}