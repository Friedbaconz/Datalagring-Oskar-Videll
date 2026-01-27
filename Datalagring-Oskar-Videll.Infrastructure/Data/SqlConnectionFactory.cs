namespace Datalagring_Oskar_Videll.Infrastructure.Data;
using Microsoft.Data.SqlClient;

public sealed class SqlConnectionFactory(string connectionString)
{
    public async Task<SqlConnection> CreateOpenConnectionAsync(CancellationToken cToken = default)
    {
        var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cToken);
        return connection;
    }
}
