using Controlador_de_comandas.Data;
using Controlador_de_comandas.Models;
using Microsoft.EntityFrameworkCore;

namespace Controlador_de_comandas.EndPoints
{
    public static class MesaEndPoints
    {
        public static void MapMesaEndPoints(this WebApplication app)
        {
            app.MapPost("/mesas", async (Mesa mesa, AppDbContext db) =>
            {
                db.Mesas.Add(mesa);
                await db.SaveChangesAsync();

                return Results.Created($"/mesas/{mesa.Id}", mesa);
            });

            app.MapGet("/mesas", async (AppDbContext db) =>
            {
                var mesas = await db.Mesas.ToListAsync();
                return Results.Ok(mesas);
            });

            app.MapGet("/mesas/{id}", async (int id, AppDbContext db) =>
            {
                var mesa = await db.Mesas.FindAsync(id);

                if (mesa == null)
                    return Results.NotFound();

                return Results.Ok(mesa);
            });

            app.MapPut("/mesas/{id}", async (int id, Mesa mesa, AppDbContext db) =>
            {
                var mesaAchada = await db.Mesas.FindAsync(id);

                if (mesaAchada == null)
                    return Results.NotFound();

                mesaAchada.Numero = mesa.Numero;
                mesaAchada.QuantidadeMax = mesa.QuantidadeMax;
                mesaAchada.Status = mesa.Status;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/mesas/{id}", async (int id, AppDbContext db) =>
            {
                var mesa = await db.Mesas.FindAsync(id);

                if (mesa == null)
                    return Results.NotFound();

                db.Mesas.Remove(mesa);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
