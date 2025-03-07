using Dapper;
using Microsoft.Data.SqlClient;

namespace AspireExample.Player;

public static class ApiEndpoints
{
    public static WebApplication MapTeamApiEndpoints(this WebApplication app)
    {
        app.MapGet("/teams", async (SqlConnection db) =>
        {
            const string sql = """
                SELECT Id, Name, City
                FROM [dbo].[Team]
                """;

            return await db.QueryAsync<Team>(sql);
        });

        return app;
    }
}

public record Team(int Id, string Name, string City);