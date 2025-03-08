using AspireExample.Aggregator.Clients;

namespace AspireExample.Aggregator;

public static class ApiEndpoints
{
    public static WebApplication MapAggregatorApiEndpoints(this WebApplication app)
    {
        app.MapGet("/aggregate", async (PlayerServiceClient playerServiceClient, TeamServiceClient teamServiceClient) =>
        {
            // cache api responses in redis and use the cache responses

            var playersTask = playerServiceClient.GetPlayers();
            var teamsTask = teamServiceClient.GetTeams();

            await Task.WhenAll(playersTask, teamsTask);

            var players = playersTask.Result.ToList();
            var teams = teamsTask.Result.ToList();
            var rand = new Random();

            return players.Select(player => 
            {
                var team = teams[rand.Next(teams.Count)];
                return new ViewModel(player, team);
            });
        });

        return app;
    }
}

public record ViewModel(Player player, Team team);