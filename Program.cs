using Controlador_de_comandas.Data;
using Controlador_de_comandas.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

//Configura o contexto do banco de dados para usar SQLite, especificando o arquivo de banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=controlador_comandas.db"));

var app = builder.Build();

//Garantir que o banco de dados seja criado ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
        app.MapOpenApi();
        app.MapScalarApiReference();
}

app.MapPost("/mesas", async (Mesa mesa, AppDbContext db) =>
{
    db.Mesas.Add(mesa);
    await db.SaveChangesAsync();

    return Results.Created($"/mesas/{mesa.Id}", mesa);
});

app.MapGet("/mesas", async (AppDbContext db) =>
{
    return await db.Mesas.ToListAsync();
});

app.Run();