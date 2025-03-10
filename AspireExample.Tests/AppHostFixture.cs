using Aspire.Hosting;
using Microsoft.Extensions.Hosting;
using AspireExample.Constants;

namespace AspireExample.Tests;

public class AppHostFixture : IDisposable, IAsyncLifetime
{
    public DistributedApplication App;
    public ResourceNotificationService ResourceNotificationService;
    public HttpClient FrontendHttpClient, TeamHttpClient, PlayerHttpClient;

    public AppHostFixture()
    {
    }

    public void Dispose() => App.Dispose();

    public async Task DisposeAsync()
    {
        await App.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AspireExample_AppHost>();

        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        App = await appHost.BuildAsync();

        await App.StartAsync();

        ResourceNotificationService = App.Services.GetRequiredService<ResourceNotificationService>();

        FrontendHttpClient = App.CreateHttpClient(ServiceNames.Frontend);
        await ResourceNotificationService.WaitForResourceAsync(ServiceNames.Frontend, KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

        TeamHttpClient = App.CreateHttpClient(ServiceNames.TeamApi);
        await ResourceNotificationService.WaitForResourceAsync(ServiceNames.TeamApi, KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

        PlayerHttpClient = App.CreateHttpClient(ServiceNames.PlayerApi);
        await ResourceNotificationService.WaitForResourceAsync(ServiceNames.PlayerApi, KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
    }
}
