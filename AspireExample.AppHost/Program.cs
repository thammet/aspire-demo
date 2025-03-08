using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .WithDockerfile("../AspireExample.Db") // start sql server with custom dockerfile
    // persist sql state
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var sqlDb = sqlServer.AddDatabase("AspireExample");

var playerApi = builder.AddProject<Projects.AspireExample_Player>("playerapi")
    .WithReference(sqlDb)
    .WaitFor(sqlDb);

var teamApi = builder.AddProject<Projects.AspireExample_Team>("teamapi")
    .WithReference(sqlDb)
    .WaitFor(sqlDb);

var frontend = builder.AddNpmApp("frontend", "../AspireExample.Web", "dev") 
    //builder.AddDockerfile("frontend", "../AspireExample.Web")
    .WithReference(playerApi)
    .WaitFor(playerApi)
    .WithReference(teamApi)
    .WaitFor(teamApi)
    .WithHttpEndpoint(port: 80, targetPort: 8080, env: "PORT")
    .WithExternalHttpEndpoints()
    // set custom env variables for app to use more easily 
    .WithEnvironment("PLAYER_API", playerApi.GetEndpoint("http")) 
    .WithEnvironment("TEAM_API", teamApi.GetEndpoint("http"))

    //.WithBuildArg("PLAYER_API", playerApi.GetEndpoint("http"))
    //.WithBuildArg("TEAM_API", teamApi.GetEndpoint("http"))
    //.WithBuildArg("PLAYER_API", playerApi)
    //        .WithBuildArg("TEAM_API", teamApi)

    .PublishAsDockerFile(options =>
    {
        // set custom env variables for app to use more easily 
        options

            .WithBuildArg("PLAYER_API", playerApi)
            .WithBuildArg("TEAM_API", teamApi);
    })
    ;

builder.Build().Run();

