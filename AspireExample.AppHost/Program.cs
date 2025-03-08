using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

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

var aggregatorApi = builder.AddProject<Projects.AspireExample_Aggregator>("aggregatorapi")
    .WithReference(playerApi)
    .WaitFor(playerApi)
    .WithReference(teamApi)
    .WaitFor(teamApi)
    .WithReference(cache);

var frontend = builder.AddNpmApp("frontend", "../AspireExample.Web", "dev") 
    .WithReference(aggregatorApi)
    .WaitFor(aggregatorApi)
    .WithHttpEndpoint(targetPort: 3000)
    .WithExternalHttpEndpoints()
    // set custom env variables for app to use more easily 
    .WithEnvironment("AGGREGATOR_API", aggregatorApi.GetEndpoint("http")) 
    .PublishAsDockerFile(options =>
    {
        // set custom env variables for app to use more easily 
        options
            .WithBuildArg("AGGREGATOR_API", aggregatorApi);
    })
    ;

builder.Build().Run();

