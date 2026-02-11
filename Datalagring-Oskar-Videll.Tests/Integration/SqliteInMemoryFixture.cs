using DatalagringOskarVidell.Application.Contracts;
using DatalagringOskarVidell.Domain.Entities;
using DatalagringOskarVidell.Domain.Models.Deltagare;
using DatalagringOskarVidell.Infrastructure.Repository;
using NSubstitute;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using DatalagringOskarVidell.Infrastructure.Data;

namespace Datalagring_Oskar_Videll.Tests.Integration;

public sealed class SqliteInMemoryFixture : IAsyncLifetime
{
    private SqliteConnection? _connection;

    public DbContextOptions<DeltagareDBContext> Options { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        _connection = new SqliteConnection("Data Source=InMemorySample;Mode=Memory;Cache=Shared");
        await _connection.OpenAsync();

        Options = new DbContextOptionsBuilder<DeltagareDBContext>()
            .UseSqlite(_connection)
            .EnableSensitiveDataLogging()
            .Options;

        await using var db = new DeltagareDBContext(Options);
        await db.Database.EnsureCreatedAsync();
    }


    public async Task DisposeAsync()
    {
        if (_connection is not null)
            await _connection.DisposeAsync();
    }

    public DeltagareDBContext CreatedDbContext() => new(Options);

}

[CollectionDefinition(Name)]

public sealed class SqlLiteinMemoryCollection : ICollectionFixture<SqliteInMemoryFixture>
{
    public const string Name = "SqliteInMemory";
}