using DatalagringOskarVidell.Application.Contracts;
using DatalagringOskarVidell.Domain.Entities;
using DatalagringOskarVidell.Domain.Models.Deltagare;
using DatalagringOskarVidell.Domain.Models.Kurs;
using DatalagringOskarVidell.Domain.Models.KursRegi;
using DatalagringOskarVidell.Domain.Models.KursRegi.LarareRegi;
using DatalagringOskarVidell.Domain.Models.KursTillfallen;
using DatalagringOskarVidell.Domain.Models.Larare;
using DatalagringOskarVidell.Domain.Models.Ort;
using DatalagringOskarVidell.Infrastructure.Data;
using DatalagringOskarVidell.Infrastructure.Repository;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DeltagareDBContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("SqlServer"), 
    sql => sql.MigrationsAssembly("Datalagring-Oskar-Videll.Infrastructure")
));

builder.Services.AddScoped<IDeltagareRepository, DeltagareRepository>();

builder.Services.AddScoped<ILarareRepository, LarareRepository>();

builder.Services.AddScoped<IOrtRepository, OrtRepository>();

builder.Services.AddScoped<IKursRepository, KursRepository>();

builder.Services.AddScoped<IKursTillfalleRepository, KurstillfalleRepository>();

builder.Services.AddScoped<IKursRegiRepository, KursRegiRepository>();

builder.Services.AddScoped<ILarareRegiRepository, LarareRegiRepository>();


var app = builder.Build();


app.MapOpenApi();
app.UseHttpsRedirection();

//Deltagare

app.MapPost("/api/Deltagare", async (CreateDeltagareDto request, IDeltagareRepository deltagareOptions, CancellationToken Ctoken) =>
{
    var dto = new CreateDeltagareDto(request.Firstname, request.Middlename, request.Lastname, request.Email, request.Phonenumber);

    var deltagare = await deltagareOptions.CreateAsync(dto, Ctoken);

    return Results.Created($"/api/Deltagare/{deltagare.Id}", deltagare);
});


app.MapGet("/api/Deltagare", async (IDeltagareRepository deltagareOptions, CancellationToken Ctoken) =>
{
    var deltagare = await deltagareOptions.GetAllAsync(Ctoken);
    return Results.Ok(deltagare);
});

app.MapGet("/api/Deltagare/{id:guid}", async (Guid ID, IDeltagareRepository deltagareOptions, CancellationToken Ctoken) =>
{
    var deltagare = await deltagareOptions.GetByIDAsync(ID, Ctoken);
    
    return deltagare is null ? null : deltagare;
});


app.MapPut("/api/Deltagare/{email}", async (string email, UpdateDeltagareDto req, IDeltagareRepository deltagareOptions, CancellationToken Ctoken) =>
{
    var dto = new UpdateDeltagareDto(req.Id, req.Firstname, req.Middlename, req.Lastname, req.Email, req.Phonenumber, req.Antagnakurser);

    var deltagare = await deltagareOptions.UpdateAsync(email, dto, Ctoken);

    return deltagare is not null
        ? Results.Ok(deltagare)
        : Results.NotFound();

});

app.MapDelete("/api/Deltagare/{id:guid}", async (Guid Id, IDeltagareRepository deltagareOptions, CancellationToken Ctoken) =>
{
    var deleted = await deltagareOptions.DeleteAsync(Id, Ctoken);
    return deleted
        ? Results.Ok() 
        : Results.NotFound();
});

//Larare

app.MapPost("/api/Larare", async (CreateLarareDto req, ILarareRepository larareOptions, CancellationToken Ctoken) =>
{
    var dto = new CreateLarareDto(req.Email, req.Firstname, req.Middlename, req.Lastname, req.Kompentens);

    var Larare = await larareOptions.CreateAsync(dto, Ctoken);

    return Results.Created($"/api/Larare/{Larare.Email}", Larare);
});

app.MapGet("/api/Larare/", async (ILarareRepository larareOptions, CancellationToken Ctoken) =>
{
    var Larare = await larareOptions.GetAllAsync(Ctoken);

    return Results.Ok(Larare);
});
app.MapGet("/api/Larare/{email}", async (string email, ILarareRepository larareOptions, CancellationToken Ctoken) =>
{
    var larare = await larareOptions.GetByEmailAsync(email, Ctoken);

    return larare is null ? null : larare;
});

app.MapPut("/api/Larare/{email}", async(string email, UpdateLarareDto req, ILarareRepository larareOptions, CancellationToken Ctoken) =>
{
    var dto = new UpdateLarareDto(req.Email, req.Firstname, req.Middlename, req.Lastname, req.Kompentens, req.Tillfallen);

    var larare = await larareOptions.UpdateAsync(email, dto, Ctoken);

    return larare is not null
        ? Results.Ok(larare)
        : Results.NotFound();


});

app.MapDelete("/api/Larare/{email}", async (string email, ILarareRepository larareOptions, CancellationToken Ctoken) =>
{
    var deleted = await larareOptions.DeleteAsync(email, Ctoken);
    return deleted
        ? Results.Ok()
        : Results.NotFound();
});

//Ort

app.MapPost("/api/Ort", async (CreateOrtDto req,IOrtRepository ortOptions, CancellationToken Ctoken) =>
{
    var dto = new CreateOrtDto(req.Ortnamn);

    var Ort = await ortOptions.CreateAsync(dto, Ctoken);

    return Results.Created($"/api/Ort/{Ort.Ortid}", Ort);
});


app.MapGet("/api/Ort/", async (IOrtRepository ortOptions, CancellationToken Ctoken) =>
{
    var Ort = await ortOptions.GetAllAsync(Ctoken);

    return Results.Ok(Ort);
});

app.MapGet("/api/Ort/{id:int}", async (int id, IOrtRepository ortOptions, CancellationToken Ctoken) =>
{
    var Ort = await ortOptions.GetByIdAsync(id, Ctoken);

    return Ort is null ? null : Ort;
});


app.MapPut("/api/Ort/{id:int}", async (int id, UpdateOrtDto req, IOrtRepository ortOptions, CancellationToken Ctoken) =>
{
    var dto = new UpdateOrtDto(req.Ortid, req.Ortnamn, req.KursTillfalle);

    var Ort = await ortOptions.UpdateAsync(id, dto, Ctoken);

    return Ort is not null
        ? Results.Ok(Ort)
        : Results.NotFound();
});

app.MapDelete("/api/Ort/{id:int}", async (int id, IOrtRepository ortOptions, CancellationToken Ctoken) =>
{
    var deleted = await ortOptions.DeleteAsync(id, Ctoken);
    return deleted
        ? Results.Ok()
        : Results.NotFound();
});

//Kurs

app.MapPost("/api/Kurs", async (CreateKursDto req, IKursRepository kursOptions, CancellationToken Ctoken) =>
{
    var dto = new CreateKursDto(req.Kurskod, req.KursNamn, req.Description);

    var Kurs = await kursOptions.CreateAsync(dto, Ctoken);

    return Results.Created($"/api/Kurs/{Kurs.Kurskod}", Kurs);
});


app.MapGet("/api/Kurs/", async (IKursRepository kursOptions, CancellationToken Ctoken) =>
{
    var Kurs = await kursOptions.GetAllAsync(Ctoken);

    return Results.Ok(Kurs);
});

app.MapGet("/api/Kurs/{Kurskod}", async (string kurskod, IKursRepository kursOptions, CancellationToken Ctoken) =>
{
    var Kurs = await kursOptions.GetByKursAsync(kurskod, Ctoken);

    return Kurs is null ? null : Kurs;
});

app.MapPut("/api/Kurs/{Kurskod}", async (string kurskod, UpdateKursDto req, IKursRepository kursOptions, CancellationToken Ctoken) =>
{
    var dto = new UpdateKursDto(req.Kurskod, req.KursNamn, req.Description, req.Kurstillfalle);

    var Kurs = await kursOptions.UpdateAsync(kurskod, dto, Ctoken);

    return Kurs is not null
        ? Results.Ok(Kurs)
        : Results.NotFound();
});

app.MapDelete("/api/Kurs/{Kurskod}", async (string kurskod, IKursRepository kursOptions, CancellationToken Ctoken) =>
{
    var deleted = await kursOptions.DeleteAsync(kurskod, Ctoken);
    return deleted
        ? Results.Ok()
        : Results.NotFound();
});

//KursTillfallen

app.MapPost("/api/Kurstillfalle", async (CreateKurstillfalleDto req, IKursTillfalleRepository tillfalleOptions, CancellationToken Ctoken) =>
{
    var dto = new CreateKurstillfalleDto(req.Kurskod, req.Startdatum, req.Slutdatum, req.Maxseats, req.OrtId);

    var Tillfalle = await tillfalleOptions.CreateAsync(dto, Ctoken);

    return Results.Created($"/api/Kurstillfalle/{Tillfalle?.KursTillfallenId}", Tillfalle);
});


app.MapGet("/api/Kurstillfalle/", async (IKursTillfalleRepository tillfalleOptions, CancellationToken Ctoken) =>
{
    var Tillfalle = await tillfalleOptions.GetAllAsync(Ctoken);

    return Results.Ok(Tillfalle);
});

app.MapGet("/api/Kurstillfalle/{KursTillfallenId:guid}", async (Guid KursTillfallenId, IKursTillfalleRepository tillfalleOptions, CancellationToken Ctoken) =>
{
    var Tillfalle = await tillfalleOptions.GetByIdAsync(KursTillfallenId, Ctoken);

    return Tillfalle is null ? null : Tillfalle;
});

app.MapPut("/api/Kurstillfalle/{KursTillfallenId:guid}", async (Guid KursTillfallenId, UpdateKurstillfalleDto req,IKursTillfalleRepository tillfalleOptions, CancellationToken Ctoken) =>
{
    var dto = new UpdateKurstillfalleDto(req.KursTillfallenId, req.KursKod, req.Kurs, req.Startdatum, req.Slutdatum, req.Maxseats, req.Ortid, req.Ort, req.LarareEmail);

    var Tillfalle = await tillfalleOptions.UpdateAsync(KursTillfallenId, dto, Ctoken);

    return Tillfalle is not null
        ? Results.Ok(Tillfalle)
        : Results.NotFound();
});

app.MapDelete("/api/Kurstillfalle/{KursTillfallenId:guid}", async (Guid KursTillfallenId, IKursTillfalleRepository tillfalleOptions, CancellationToken Ctoken) =>
{
    var deleted = await tillfalleOptions.DeleteAsync(KursTillfallenId, Ctoken);
    return deleted
        ? Results.Ok()
        : Results.NotFound();
});

//KursRegi

app.MapPost("/api/KursRegi", async (CreateKursRegiDto req, IKursRegiRepository RegiOptions, CancellationToken Ctoken) =>
{
    var dto = new CreateKursRegiDto(req.RegiID, req.Antagen, req.RegistrationDate, req.Status);

    var Regi = await RegiOptions.CreateAsync(dto, Ctoken);

    return Results.Created($"/api/KursRegi/{Regi?.RegiID}", Regi);
});

app.MapGet("/api/KursRegi/", async (IKursRegiRepository RegiOptions, CancellationToken Ctoken) =>
{
    var Regi = await RegiOptions.GetAllAsync(Ctoken);

    return Results.Ok(Regi);
});

app.MapGet("/api/KursRegi/{KursRegiId:guid}", async (Guid KursRegiId, IKursRegiRepository RegiOptions, CancellationToken Ctoken) =>
{
    var Regi = await RegiOptions.GetByIDAsync(KursRegiId,Ctoken);


    return Regi is null ? null : Regi;
});

app.MapPut("/api/KursRegi/{KursRegiId:guid}", async (Guid KursRegiId, UpdateKursRegiDto req, IKursRegiRepository RegiOptions, CancellationToken Ctoken) =>
{
    var dto = new UpdateKursRegiDto(req.KursRegiId, req.Antagen, req.RegistrationDate, req.Status, req.DeltagareRegi, req.Kurstillfallen);

    var Regi = await RegiOptions.UpdateAsync(KursRegiId, dto, Ctoken);

    return Regi is not null
        ? Results.Ok(Regi)
        : Results.NotFound();
});

app.MapDelete("/api/KursRegi/{KursRegiId:guid}", async (Guid KursRegiId, IKursRegiRepository RegiOptions, CancellationToken Ctoken) =>
{
    var deleted = await RegiOptions.DeleteAsync(KursRegiId, Ctoken);
    return deleted
        ? Results.Ok()
        : Results.NotFound();
});

//LarareRegi

app.MapPost("/api/LarareRegi", async (CreateLarareRegiDto req, ILarareRegiRepository RegiOptions, CancellationToken Ctoken) =>
{
    var dto = new CreateLarareRegiDto(req.LarareRegiId, req.LarareEmail);

    var Regi = await RegiOptions.CreateAsync(dto, Ctoken);

    return Results.Created($"/api/KursRegi/{Regi?.LarareRegiId}", Regi);
});

app.MapGet("/api/LarareRegi/", async (ILarareRegiRepository RegiOptions, CancellationToken Ctoken) =>
{
    var Regi = await RegiOptions.GetAllAsync(Ctoken);

    return Results.Ok(Regi);
});

app.MapGet("/api/LarareRegi/{LarareRegiId:guid}", async (Guid LarareRegiId, ILarareRegiRepository RegiOptions, CancellationToken Ctoken) =>
{
    var Regi = await RegiOptions.GetByIdAsync(LarareRegiId, Ctoken);


    return Regi is null ? null : Regi;
});

app.MapPost("/api/LarareRegi/{LarareRegiId:guid}", async (Guid LarareRegiId, UpdateLarareRegiDto req, ILarareRegiRepository RegiOptions, CancellationToken Ctoken) =>
{
    var dto = new UpdateLarareRegiDto(req.LarareRegiId, req.LarareEmail, req.LarareRegi, req.Kurstillfallen);

    var Regi = await RegiOptions.UpdateAsync(LarareRegiId, dto, Ctoken);

    return Regi is not null
        ? Results.Ok(Regi)
        : Results.NotFound();
});

app.MapDelete("/api/LarareRegi/{LarareRegiId:guid}", async (Guid LarareRegiId, ILarareRegiRepository RegiOptions, CancellationToken Ctoken) =>
{
    var deleted = await RegiOptions.DeleteAsync(LarareRegiId, Ctoken);
    return deleted
        ? Results.Ok()
        : Results.NotFound();
});


app.Run();
