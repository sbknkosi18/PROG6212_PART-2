using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using Xunit;

public class EnrollmentTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EnrollmentTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Participant_CanEnrollInEvent()
    {
        var token = await TestHelper.AuthenticateAsync(_client, "participant@example.com", "Password123!", "Participant");
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _client.PostAsJsonAsync("/api/enrollments", new
        {
            EventId = 1,
            CategoryId = 1
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Organiser_CanViewEnrollments()
    {
        var token = await TestHelper.AuthenticateAsync(_client, "organiser@example.com", "Password123!", "Organiser");
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync("/api/enrollments/1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

