using AspireExample.Aggregator;
using AspireExample.Aggregator.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddRedisClient(connectionName: "cache");

builder.Services.AddHttpClient<PlayerServiceClient>(
    static client => client.BaseAddress = new("https+http://playerapi"));
    
builder.Services.AddHttpClient<TeamServiceClient>(
    static client => client.BaseAddress = new("https+http://teamapi"));

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
