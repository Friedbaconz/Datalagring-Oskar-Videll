using System.Data;

namespace DatalagringOskarVidell.Infrastructure.Data;

public sealed class sqlConnectionFactory(string connectionString)
{
    public async Task<IDbConnection> CreateOpenConnectionAsync()
    {
        var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }

}
