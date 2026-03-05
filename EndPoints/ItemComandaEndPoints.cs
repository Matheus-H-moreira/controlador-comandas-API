using Controlador_de_comandas.Data;
using Controlador_de_comandas.Models;
using Microsoft.EntityFrameworkCore;

namespace Controlador_de_comandas.EndPoints
{
    public static class ItemComandaEndPoints
    {
        public static void MapItemComandaEndPoints(this WebApplication app)
        {
            app.MapPost("/item", async (ItemComanda item, AppDbContext db) => 
            {
                if (!await db.Comandas.AnyAsync(c => c.Id == item.ComandaId))
                    return Results.NotFound();

                if (!await db.Produtos.AnyAsync(p => p.NomeProduto == item.NomeProdutoPedido))
                    return Results.NotFound();

                db.ItensComanda.Add(item);
                await db.SaveChangesAsync();

                return Results.Created($"/item/{item.Id}", item);
            });

            app.MapGet("/item", async (AppDbContext db) =>
            {
                var itens = await db.ItensComanda.ToListAsync();
                return Results.Ok(itens);
            });

            app.MapPut("/item/{id}", async (int id, ItemComanda item, AppDbContext db) =>
            {
                var itemAchado = await db.ItensComanda.FindAsync(id);

                if (itemAchado == null)
                    return Results.NotFound();

                itemAchado.NomeProdutoPedido = item.NomeProdutoPedido;
                itemAchado.QuantidadeProduto = item.QuantidadeProduto;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/item/{id}", async (int id, AppDbContext db) =>
            {
                var item = await db.ItensComanda.FindAsync(id);

                if (item == null)
                    return Results.NotFound();

                db.ItensComanda.Remove(item);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
