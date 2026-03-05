using Controlador_de_comandas.Data;
using Controlador_de_comandas.EndPoints;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

//Configura o contexto do banco de dados para usar SQLite, especificando o arquivo de banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=controlador_comandas.db"));

var app = builder.Build();

//Garantir que o banco de dados seja criado ao iniciar a aplicação, caso ele não exista 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

//Habilita a documentação da API apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
        app.MapOpenApi();
        app.MapScalarApiReference();
}

//Mapeia os endpoints separados por responsabilidade
app.MapMesaEndPoints();
app.MapProdutoEndPoints();
app.MapComandaEndPoints();
app.MapItemComandaEndPoints();

app.Run();