using Microsoft.Data.SqlClient;
using Datalagring_Oskar_Videll.Infrastructure.Data;
using Datalagring_Oskar_Videll.Application.Interfaces;
using Datalagring_Oskar_Videll.Infrastructure.Repositiories;
using Datalagring_Oskar_Videll.Presentation.Api.Models;
using Datalagring_Oskar_Videll.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton(new SqlConnectionFactory(builder.Configuration.GetConnectionString("SqlServer")!));
builder.Services.AddScoped<IDeltagare_Repository, Deltagare_Repository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/api/deltagare", async (CreateDeltagareRequest request, IDeltagare_Repository deltagare_Repository, CancellationToken cToken) =>
{
    var Dto = new CreateDeltagareDto(
        request.fornamn,
        request.mellannamn,
        request.efternamn,
        request.email,
        request.telefonnummer);
    var deltagare =  await deltagare_Repository.CreateDeltagareAsync(Dto, cToken);
    return Results.Created($"/api/deltagare/{deltagare.email}", deltagare);
});

app.MapGet("/api/deltagare", async (IDeltagare_Repository deltagare_Repository, CancellationToken cToken) =>
{
    var deltagare = await deltagare_Repository.GetAllAsync(cToken);
    return Results.Ok(deltagare);
});

app.MapGet("/api/deltagare/{email:string}", async (string email, IDeltagare_Repository deltagare_Repository, CancellationToken cToken) =>
{
    var deltagare = await deltagare_Repository.GetDeltagareByEmailAsync(email, cToken);
    return deltagare is not null 
        ? Results.Ok(deltagare) 
        : Results.NotFound();
});

app.MapPut("/apit/deltagare/{email:string}", async (string email, UpdateDeltagareDto request, IDeltagare_Repository deltagare_Repository, CancellationToken cToken) =>
{
    var dto = new UpdateDeltagareDto(
        request.fornamn,
        request.mellannamn,
        request.efternamn,
        request.email,
        request.telefonnummer);
    var updatedDeltagare = await deltagare_Repository.UpdateDeltagareAsync(dto, cToken);
    return updatedDeltagare is not null 
        ? Results.Ok(updatedDeltagare) 
        : Results.NotFound();
});

app.MapDelete("/api/deltagare/{email:string}", async (string email, IDeltagare_Repository deltagare_Repository, CancellationToken cToken) =>
{
    var deleted =  await deltagare_Repository.DeleteDeltagareAsync(email, cToken);
    return deleted 
        ? Results.NoContent() 
        : Results.NotFound();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
