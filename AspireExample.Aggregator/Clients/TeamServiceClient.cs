namespace AspireExample.Aggregator.Clients;

public class TeamServiceClient 
{
    private readonly HttpClient _httpClient;

    public TeamServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Team>> GetTeams() =>
        await _httpClient.GetFromJsonAsync<IEnumerable<Team>>("teams");
}

public record Team(int Id, string Name, string City);
