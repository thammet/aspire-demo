namespace AspireExample.Aggregator.Clients;

public class PlayerServiceClient 
{
    private readonly HttpClient _httpClient;

    public PlayerServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Player>> GetPlayers() =>
        await _httpClient.GetFromJsonAsync<IEnumerable<Player>>("players");
}

public record Player(int Id, string FirstName, string LastName);