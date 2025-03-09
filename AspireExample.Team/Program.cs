using AspireExample.Constants;
using AspireExample.Player;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddSqlServerClient(ServiceNames.SqlDb);

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

app.MapTeamApiEndpoints();

app.Run();
