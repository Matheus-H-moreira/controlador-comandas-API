using Controlador_de_comandas.Data;
using Controlador_de_comandas.Models;
using Microsoft.EntityFrameworkCore;

namespace Controlador_de_comandas.EndPoints
{
    public static class ComandaEndPoints
    {
        public static void MapComandaEndPoints(this WebApplication app)
        {
            app.MapPost("/comandas", async (Comanda comanda, AppDbContext db) =>
            {
                if (!await db.Mesas.AnyAsync(m => m.Id == comanda.MesaId))
                    return Results.NotFound();

                comanda.Status = "Aberta";
                comanda.DataAbertura = DateTime.Now;

                db.Add(comanda);
                await db.SaveChangesAsync();

                return Results.Created($"/comandaS/{comanda.Id}", comanda);
            });

            app.MapGet("/comandas", async (AppDbContext db) =>
            {
                var comandas = await db.Comandas.ToListAsync();
                return Results.Ok(comandas);
            });

            app.MapGet("/comandas/{id}", async (int id, AppDbContext db) =>
            {
                var comanda = await db.Comandas.FindAsync(id);

                if (comanda == null)
                    return Results.NotFound();

                return Results.Ok(comanda);
            });

            app.MapPatch("/comandas/{id}/status", async (int id, string status, AppDbContext db) =>
            {
                var comanda = await db.Comandas.FindAsync(id);

                if (comanda == null)
                    return Results.NotFound();

                comanda.Status = status;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
