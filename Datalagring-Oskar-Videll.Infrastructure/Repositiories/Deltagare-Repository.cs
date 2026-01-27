using Dapper;
using Datalagring_Oskar_Videll.Domain.Models;
using Datalagring_Oskar_Videll.Infrastructure.Data;
using Datalagring_Oskar_Videll.Application.Interfaces;

namespace Datalagring_Oskar_Videll.Infrastructure.Repositiories;

public sealed class Deltagare_Repository(SqlConnectionFactory factory) : IDeltagare_Repository
{
    public async Task<Deltagare> CreateDeltagareAsync(CreateDeltagareDto deltagare, CancellationToken cToken = default)
    {
        await using var connection = await factory.CreateOpenConnectionAsync(cToken);


        string query = """
            INSERT INTO Deltagare (fornamn, mellannamn, efternamn, email, telefonnummer) 

            OUTPUT 
                INSERTED.Email, 
                INSERTED.Fornamn,
                INSERTED.Mellannamn,
                INSERTED.Efternamn,
                INSERTED.Telefonnummer

            VALUES ('@fornamn', '@mellannamn', '@efternamn', '@email', '@telefonnummer');
            """;

        return await connection.QuerySingleAsync<Deltagare>(new(query, deltagare, cancellationToken: cToken));
    }

    public async Task<Deltagare?> GetDeltagareByEmailAsync(string email, CancellationToken cToken = default)
    {
        await using var connection = await factory.CreateOpenConnectionAsync(cToken);
        string query = """
            SELECT 
                Email, 
                Fornamn,
                Mellannamn,
                Efternamn,
                Telefonnummer
            FROM Deltagare
            WHERE Email = '@email';
            """;
        return await connection.QuerySingleOrDefaultAsync<Deltagare>(new(query, new { Email = email }, cancellationToken: cToken));
    }

    public async Task<IReadOnlyList<Deltagare>> GetAllAsync(CancellationToken cToken = default)
    {
        await using var connection = await factory.CreateOpenConnectionAsync(cToken);
        string query = """
            SELECT 
                Email, 
                Fornamn,
                Mellannamn,
                Efternamn,
                Telefonnummer
            FROM Deltagare
            WHERE Email = '@email';
            """;
        var data = await connection.QueryAsync<Deltagare>(new(query, cancellationToken: cToken));
        return [.. data];
    }

    public async Task<Deltagare?> UpdateDeltagareAsync(UpdateDeltagareDto deltagare, CancellationToken cToken)
    {
        await using var connection = await factory.CreateOpenConnectionAsync(cToken);
        string query = """
            UPDATE Deltagare
            SET 
                Fornamn = '@fornamn',
                Mellannamn = '@mellannamn',
                Efternamn = '@efternamn',
                Telefonnummer = '@telefonnummer'

            WHERE Email = '@email';

            OUTPUT 
                INSERTED.Email, 
                INSERTED.Fornamn,
                INSERTED.Mellannamn,
                INSERTED.Efternamn,
                INSERTED.Telefonnummer;
            """;
        return await connection.QuerySingleOrDefaultAsync<Deltagare>(new(query, deltagare, cancellationToken: cToken));
    }

    public async Task<bool> DeleteDeltagareAsync(string email, CancellationToken cToken)
    {
        await using var connection = await factory.CreateOpenConnectionAsync(cToken);
        string query = """
            DELETE FROM Deltagare
            WHERE Email = '@email';

            """;
        var affectedRows = await connection.ExecuteAsync(new(query, new { Email = email }, cancellationToken: cToken));
        return affectedRows == 1;
    }

}
