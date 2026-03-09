using Controlador_de_comandas.Data;
using Controlador_de_comandas.Models;
using Microsoft.EntityFrameworkCore;
using Controlador_de_comandas.DTOs.MesaDTOs;

namespace Controlador_de_comandas.EndPoints
{
    public static class MesaEndPoints
    {
        public static void MapMesaEndPoints(this WebApplication app)
        {
            //Cria uma nova mesa
            app.MapPost("/mesas", async (CreateMesaDTO dto, AppDbContext db) =>
            {
                var mesa = new Mesa
                {
                    Numero = dto.NumeroMesa,
                    QuantidadeMax = dto.QuantidadeMaxPessoas,
                    Status = dto.Status
                };
                
                db.Mesas.Add(mesa);
                await db.SaveChangesAsync();

                return Results.Created($"/mesas/{mesa.Id}", mesa);
            });

            //Lista todas as mesas cadastradas
            app.MapGet("/mesas", async (AppDbContext db) =>
            {
                var mesas = await db.Mesas.AsNoTracking().ToListAsync();
                return Results.Ok(mesas);
            });

            //Busca uma mesa específica pelo Id
            app.MapGet("/mesas/{id}", async (int id, AppDbContext db) =>
            {
                var mesa = await db.Mesas.FindAsync(id);

                if (mesa == null)
                    return Results.NotFound();

                return Results.Ok(mesa);
            });

            app.MapGet("/mesas/{idMesa}/comandas", async (int idMesa, AppDbContext db) =>
            {
                var comandas = db.Comandas.Where(c => c.MesaId == idMesa).ToListAsync();

                return Results.Ok(comandas);
            });

            //Atualiza o número de uma mesa
            app.MapPatch("/mesas/numero/{id}", async (int id, UpdateNumMesaDTO dto, AppDbContext db) =>
            {
                var mesaAchada = await db.Mesas.FindAsync(id);

                if (mesaAchada == null)
                    return Results.NotFound();

                mesaAchada.Numero = dto.NumeroMesa;
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            //Atualiza o número de pessoas máxima de uma mesa
            app.MapPatch("/mesas/quantidadePessoas/{id}", async (int id, UpdatePessoasMesaDTO dto, AppDbContext db) =>
            {
                var mesaAchada = await db.Mesas.FindAsync(id);

                if (mesaAchada == null)
                    return Results.NotFound();

                mesaAchada.QuantidadeMax = dto.QuantidadeMaxPessoas;
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            //Atualiza o status de uma mesa
            app.MapPatch("/mesas/status/{id}", async (int id, UpdateStatusDTO dto, AppDbContext db) =>
            {
                var mesaAchada = await db.Mesas.FindAsync(id);

                if (mesaAchada == null)
                    return Results.NotFound();

                mesaAchada.Status = dto.Status;
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            //Remove uma mesa do sistema
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
