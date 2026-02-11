using DatalagringOskarVidell.Domain.Entities;
using DatalagringOskarVidell.Domain.Models.Deltagare;
using DatalagringOskarVidell.Domain.Models.KursTillfallen;
using DatalagringOskarVidell.Infrastructure.Data;
using DatalagringOskarVidell.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datalagring_Oskar_Videll.Tests.Integration.Infrastructure;

[Collection(SqlLiteinMemoryCollection.Name)]
public sealed class UserRepository_Test(SqliteInMemoryFixture fixture)
{
    [Fact]

    public async Task CreateAsync_Should_Return_Id()
    {
        await using var db = fixture.CreatedDbContext();
        await ClearUsersAsync(db);

        var repo = new DeltagareRepository(db);

        var id = await repo.CreateAsync(new CreateDeltagareDto
        (
            Firstname: "hey",
            Middlename: "nej",
            Lastname: "jej",
            Email: "nej@email.se",
            Phonenumber: "123456789"
        ), CancellationToken.None);

        Assert.True(id != Guid.Empty);

        var all = await repo.GetAllAsync(CancellationToken.None);
        Assert.Single(all);
        Assert.Equal(id, all[0].Id);
        Assert.Equal("hey", all[0].Firstname);
        Assert.Equal("nej", all[0].Middlename);
        Assert.Equal("jej", all[0].Lastname);
        Assert.Equal("nej@email.se", all[0].Email);
        Assert.Equal("123456789", all[0].Phonenumber);

    }

    [Fact]

    public async Task Update_Async_Should_Return_New_Name()
    {
        await using var db = fixture.CreatedDbContext();
        await ClearUsersAsync(db);

        var repo = new DeltagareRepository(db);

        var id = await repo.CreateAsync(new CreateDeltagareDto
        (
            Firstname: "hey",
            Middlename: "nej",
            Lastname: "jej",
            Email: "nej@email.se",
            Phonenumber: "123456789"
        ), CancellationToken.None);

        Assert.True(id != Guid.Empty);

        var test = await repo.UpdateAsync(id, new UpdateDeltagareDto
            (
                Id: id,
                Firstname: "bob",
                Middlename: "nej",
                Lastname: "jej",
                Email: "nej@email.se",
                Phonenumber: "123456789",
                Antagnakurser: []
            ), CancellationToken.None);

        var all = await repo.GetAllAsync(CancellationToken.None);
        Assert.Single(all);
        Assert.Equal(id, all[0].Id);
        Assert.Equal("bob", all[0].Firstname);
        Assert.Equal("nej", all[0].Middlename);
        Assert.Equal("jej", all[0].Lastname);
        Assert.Equal("nej@email.se", all[0].Email);
        Assert.Equal("123456789", all[0].Phonenumber);
    }

    [Fact]
    public async Task GetByID_Should_Return_Requested_Profile()
    {
        await using var db = fixture.CreatedDbContext();
        await ClearUsersAsync(db);

        var repo = new DeltagareRepository(db);

        var id = await repo.CreateAsync(new CreateDeltagareDto
        (
            Firstname: "hey",
            Middlename: "nej",
            Lastname: "jej",
            Email: "nej@email.se",
            Phonenumber: "123456789"
        ), CancellationToken.None);

        Assert.True(id != Guid.Empty);

        var byid = await repo.GetByIDAsync(id, CancellationToken.None);

        Assert.True(id == byid!.Id);
    }

    [Fact]
    public async Task Delete_Should_Return_True()
    {
        await using var db = fixture.CreatedDbContext();
        await ClearUsersAsync(db);

        var repo = new DeltagareRepository(db);

        var id = await repo.CreateAsync(new CreateDeltagareDto
        (
            Firstname: "hey",
            Middlename: "nej",
            Lastname: "jej",
            Email: "nej@email.se",
            Phonenumber: "123456789"
        ), CancellationToken.None);

        var Delete = await repo.DeleteAsync(id, CancellationToken.None);

        Assert.True(Delete = true);
    }

    [Fact]

    public async Task GetAll_Should_Return_The_Profile()
    {
        await using var db = fixture.CreatedDbContext();
        await ClearUsersAsync(db);

        var repo = new DeltagareRepository(db);

        var id = await repo.CreateAsync(new CreateDeltagareDto
        (
            Firstname: "hey",
            Middlename: "nej",
            Lastname: "jej",
            Email: "nej@email.se",
            Phonenumber: "123456789"
        ), CancellationToken.None);

        Assert.True(id != Guid.Empty);

        var all = await repo.GetAllAsync(CancellationToken.None);
        Assert.Single(all);
        Assert.Equal(id, all[0].Id);
        Assert.Equal("hey", all[0].Firstname);
        Assert.Equal("nej", all[0].Middlename);
        Assert.Equal("jej", all[0].Lastname);
        Assert.Equal("nej@email.se", all[0].Email);
        Assert.Equal("123456789", all[0].Phonenumber);
    }

    private static async Task ClearUsersAsync(DeltagareDBContext db)
    {
        await db.Database.ExecuteSqlRawAsync("DELETE FROM Deltagare;");
    }
}
