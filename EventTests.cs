using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using Xunit;

public class EventTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EventTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Organiser_CanCreateEvent()
    {
        var token = await TestHelper.AuthenticateAsync(_client, "organiser@example.com", "Password123!", "Organiser");
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _client.PostAsJsonAsync("/api/events", new
        {
            Title = "Test Event",
            Description = "Event description",
            Date = DateTime.Now,
            Location = "Johannesburg",
            Distance = 10.0,
            EventType = "Run"
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Participant_CannotCreateEvent()
    {
        var token = await TestHelper.AuthenticateAsync(_client, "participant@example.com", "Password123!", "Participant");
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _client.PostAsJsonAsync("/api/events", new { Title = "Invalid Event" });
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}

