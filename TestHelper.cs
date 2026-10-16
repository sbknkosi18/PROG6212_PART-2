using System.Net.Http.Json;
using System.Threading.Tasks;

public static class TestHelper
{
    public static async Task<string> AuthenticateAsync(HttpClient client, string email, string password, string role)
    {
        // Register user if not exists
        await client.PostAsJsonAsync("/api/auth/register", new
        {
            Name = "Test " + role,
            Email = email,
            Password = password,
            Role = role
        });

        // Login
        var response = await client.PostAsJsonAsync("/api/auth/login", new
        {
            Email = email,
            Password = password
        });

        var result = await response.Content.ReadFromJsonAsync<dynamic>();
        return (string)result.token;
    }
}

