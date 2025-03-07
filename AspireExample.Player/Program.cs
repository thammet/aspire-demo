using AspireExample.Player;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();

// matches AddDatabase call in AppHost
builder.AddSqlServerClient("AspireExample");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapUserApiEndpoints();

app.Run();
