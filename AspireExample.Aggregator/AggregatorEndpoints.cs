using AspireExample.Aggregator.Clients;
using StackExchange.Redis;
using System.Text.Json;

namespace AspireExample.Aggregator;

public static class ApiEndpoints
{
    public static WebApplication MapAggregatorApiEndpoints(this WebApplication app)
    {
        app.MapGet("/aggregate", async (PlayerServiceClient playerServiceClient, TeamServiceClient teamServiceClient, IDatabase cache) =>
        {           
            var playersTask = GetPlayers(cache, playerServiceClient);
            var teamsTask = GetTeams(cache, teamServiceClient);

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

    private static async Task<List<Player>> GetPlayers(IDatabase cache, PlayerServiceClient playerServiceClient)
    {
        return await GetCachedValue(cache, "players", () => playerServiceClient.GetPlayers());
    }

    private static async Task<List<Team>> GetTeams(IDatabase cache, TeamServiceClient teamServiceClient)
    {
        return await GetCachedValue(cache, "teams", () => teamServiceClient.GetTeams());
    }

    private static async Task<List<T>> GetCachedValue<T>(IDatabase cache, string key, Func<Task<IEnumerable<T>>> dataRetriever)
    {
        var cachedData = await cache.StringGetAsync(key);

        if (cachedData.HasValue) return JsonSerializer.Deserialize<List<T>>(cachedData.ToString());

        var data = (await dataRetriever()).ToList();

        await cache.StringSetAsync(key, JsonSerializer.Serialize(data));

        return data;
    }
}

public record ViewModel(Player player, Team team);