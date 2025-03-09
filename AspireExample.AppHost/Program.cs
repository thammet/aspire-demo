using Aspire.Hosting;
using AspireExample.Constants;


var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var sqlServer = builder.AddSqlServer(ServiceNames.Sql)
    .WithDockerfile("../AspireExample.Db") // start sql server with custom dockerfile
    // persist sql state
    .WithDataVolume();

var sqlDb = sqlServer.AddDatabase(ServiceNames.SqlDb);

var playerApi = builder.AddProject<Projects.AspireExample_Player>(ServiceNames.PlayerApi)
    .WithReference(sqlDb)
    .WaitFor(sqlDb);

var teamApi = builder.AddProject<Projects.AspireExample_Team>(ServiceNames.TeamApi)
    .WithReference(sqlDb)
    .WaitFor(sqlDb);

var aggregatorApi = builder.AddProject<Projects.AspireExample_Aggregator>(ServiceNames.AggregatorApi)
    .WithReference(playerApi)
    .WaitFor(playerApi)
    .WithReference(teamApi)
    .WaitFor(teamApi)
    .WithReference(cache);

var frontend = builder.AddNpmApp(ServiceNames.Frontend, "../AspireExample.Web", "dev") 
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

