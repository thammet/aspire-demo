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

var frontend = builder.AddNpmApp("frontend", "../AspireExample.Web", "dev") // dev is a script in package.json
    .WithReference(playerApi)
    .WaitFor(playerApi)
    .WithReference(teamApi)
    .WaitFor(teamApi)
    .WithHttpEndpoint(port: 80, targetPort: 8080, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile(); // use our custom dockerfile when deploying

builder.Build().Run();
