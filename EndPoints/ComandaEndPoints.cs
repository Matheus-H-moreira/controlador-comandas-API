using Controlador_de_comandas.Data;
using Controlador_de_comandas.DTOs.ComandaDTOs;
using Controlador_de_comandas.Models;
using Microsoft.EntityFrameworkCore;

namespace Controlador_de_comandas.EndPoints
{
    public static class ComandaEndPoints
    {
        public static void MapComandaEndPoints(this WebApplication app)
        {
            //Cria uma nova comanda
            app.MapPost("/comandas", async (CreateComandaDTO dto, AppDbContext db) =>
            {
                //Verifica se a mesa informada existe antes de criar a comanda
                if (!await db.Mesas.AnyAsync(m => m.Id == dto.MesaId))
                    return Results.NotFound();

                //Define automaticamente o status e a data de abertura
                Comanda comanda = new Comanda
                {
                    MesaId = dto.MesaId,
                    NomeCliente = dto.NomeCliente,
                    Status = "Aberta",
                    DataAbertura = DateTime.Now
                };

                db.Add(comanda);
                await db.SaveChangesAsync();

                return Results.Created($"/comandas/{comanda.Id}", comanda);
            });

            //Lista todas as comandas
            app.MapGet("/comandas", async (AppDbContext db) =>
            {
                var comandas = await db.Comandas.ToListAsync();
                return Results.Ok(comandas);
            });

            //Busca uma comanda específica pelo Id
            app.MapGet("/comandas/{id}", async (int id, AppDbContext db) =>
            {
                var comanda = await db.Comandas.FindAsync(id);

                if (comanda == null)
                    return Results.NotFound();

                return Results.Ok(comanda);
            });

            app.MapGet("/comandas/abertas", async (AppDbContext db) =>
            {
                var comandas = await db.Comandas.Where(c => c.Status == "Aberta").ToListAsync();

                return Results.Ok(comandas);
            });

            //Atualiza apenas o status da comanda
            app.MapPatch("/comandas/{id}/fechar", async (int id, AppDbContext db) =>
            {
                var comanda = await db.Comandas.FindAsync(id);

                if (comanda == null)
                    return Results.NotFound();

                if (comanda.Status == "Fechada")
                    return Results.BadRequest("A comanda já está fechada e não pode ser alterada.");

                comanda.Status = "Fechada";

                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
