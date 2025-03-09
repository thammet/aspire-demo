using Aspire.Hosting;
using Microsoft.Extensions.Hosting;
using AspireExample.Constants;

namespace AspireExample.Tests;

public class AppHostFixture : IDisposable
{
    public DistributedApplication App;
    public ResourceNotificationService ResourceNotificationService;
    public HttpClient FrontendHttpClient, TeamHttpClient, PlayerHttpClient;

    public AppHostFixture()
    {
        var appHost = DistributedApplicationTestingBuilder.CreateAsync<Projects.AspireExample_AppHost>(args: []).Result;

        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        App = appHost.BuildAsync().Result;

        App.Start();

        ResourceNotificationService = App.Services.GetRequiredService<ResourceNotificationService>();

        FrontendHttpClient = App.CreateHttpClient(ServiceNames.Frontend);
        ResourceNotificationService.WaitForResourceAsync(ServiceNames.Frontend, KnownResourceStates.Running).Wait(TimeSpan.FromSeconds(30));

        TeamHttpClient = App.CreateHttpClient(ServiceNames.TeamApi);
        ResourceNotificationService.WaitForResourceAsync(ServiceNames.TeamApi, KnownResourceStates.Running).Wait(TimeSpan.FromSeconds(30));

        PlayerHttpClient = App.CreateHttpClient(ServiceNames.PlayerApi);
        ResourceNotificationService.WaitForResourceAsync(ServiceNames.PlayerApi, KnownResourceStates.Running).Wait(TimeSpan.FromSeconds(30));
    }

    public void Dispose() => App.Dispose();
}
