using Aspire.Hosting;

namespace AspireExample.Tests.Tests;

public class IntegrationTest1 : IClassFixture<AppHostFixture>
{
    readonly AppHostFixture Fixture;

    public IntegrationTest1(AppHostFixture fixture) 
    {
        Fixture = fixture;
    }

    [Fact]
    public async Task Frontend_Loads()
    {
        var response = await Fixture.FrontendHttpClient.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task TeamApi_GetTeams()
    {
        var response = await Fixture.TeamHttpClient.GetAsync("/teams");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PlayerApi_GetPlayers()
    {
        var response = await Fixture.PlayerHttpClient.GetAsync("/players");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
