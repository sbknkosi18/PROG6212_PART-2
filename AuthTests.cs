using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using Xunit;

public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_NewUser_ReturnsCreated()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "Password123!",
            Role = "Participant"
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsUnauthorized()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            Email = "wrong@example.com",
            Password = "badpassword"
        });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
