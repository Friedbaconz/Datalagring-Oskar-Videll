using Microsoft.Data.SqlClient;
using Datalagring_Oskar_Videll.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DeltagareDBContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("SqlServer"), 
    sql => sql.MigrationsAssembly("Datalagring-Oskar-Videll.Infrastructure")
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
