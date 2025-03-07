using Dapper;
using Microsoft.Data.SqlClient;

namespace AspireExample.Player;

public static class ApiEndpoints
{
    public static WebApplication MapUserApiEndpoints(this WebApplication app)
    {
        app.MapGet("/players", async (SqlConnection db) =>
        {
            const string sql = """
                SELECT Id, FirstName, LastName
                FROM [dbo].[Player]
                """;

            return await db.QueryAsync<Player>(sql);
        });

        return app;
    }
}

public record Player(int Id, string FirstName, string LastName);