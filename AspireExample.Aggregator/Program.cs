using AspireExample.Aggregator;
using AspireExample.Aggregator.Clients;
using AspireExample.Constants;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddRedisClient(connectionName: ServiceNames.Cache);

builder.Services.AddHttpClient<PlayerServiceClient>(
    static client => client.BaseAddress = new($"https+http://{ServiceNames.PlayerApi}"));
    
builder.Services.AddHttpClient<TeamServiceClient>(
    static client => client.BaseAddress = new($"https+http://{ServiceNames.TeamApi}"));

builder.Services.AddScoped(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else 
{
    app.UseHttpsRedirection();
}

app.MapAggregatorApiEndpoints();

app.Run();
