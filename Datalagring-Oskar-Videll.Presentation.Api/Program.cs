using Microsoft.EntityFrameworkCore;
using DatalagringOskarVidell.Infrastructure.Repository;
using DatalagringOskarVidell.Domain.Models.Ort;
using DatalagringOskarVidell.Domain.Models.Deltagare;
using DatalagringOskarVidell.Infrastructure.Data;
using DatalagringOskarVidell.Application.Contracts;
using System.Net.Mail;
using DatalagringOskarVidell.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DeltagareDBContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("SqlServer"), 
    sql => sql.MigrationsAssembly("Datalagring-Oskar-Videll.Infrastructure")
));

builder.Services.AddScoped<IDeltagareRepository, DeltagareRepository>();



var app = builder.Build();


app.MapOpenApi();
app.UseHttpsRedirection();

var list = new List<DeltagareEntity>()
{


};

app.MapPost("/api/Deltagare", async (CreateDeltagareDto request, IDeltagareRepository deltagareOptions, CancellationToken Ctoken) =>
{
    var dto = new CreateDeltagareDto(request.Firstname, request.Middlename, request.Lastname, request.Email, request.Phonenumber);

    var deltagare = await deltagareOptions.CreateAsync(dto, Ctoken);

    return Results.Created($"/api/Deltagare/{deltagare.Id}", deltagare);
});

app.MapGet("/api/Deltagare", async (IDeltagareRepository deltagareOptions, CancellationToken Ctoken) =>
{
    var deltagare = await deltagareOptions.GetAllAsync(Ctoken);
    return Results.Ok(list);
});

//Get Deltagare

app.MapGet("/api/Deltagare/{email}", async (string email, IDeltagareRepository deltagareOptions, CancellationToken Ctoken) =>
{
    var deltagare = await deltagareOptions.GetByEmailAsync(email, Ctoken);
    
    return deltagare is null ? null : deltagare;
});

app.Run();
