using Controlador_de_comandas.Data;
using Controlador_de_comandas.Models;
using Microsoft.EntityFrameworkCore;
using Controlador_de_comandas.DTOs.ItemDTOs;

namespace Controlador_de_comandas.EndPoints
{
    public static class ItemComandaEndPoints
    {
        public static void MapItemComandaEndPoints(this WebApplication app)
        {
            //Adiciona um novo item em uma comanda
            app.MapPost("/item", async (CreateItemDTO dto, AppDbContext db) =>
            {
                var comanda = await db.Comandas.FindAsync(dto.ComandaId);

                //Verifica se a comanda existe
                if (comanda == null)
                    return Results.NotFound();

                //Impede adicionar itens em comandas fechadas
                if (comanda.Status == "Fechada")
                    return Results.BadRequest("Comanda já está fechada");

                //Verifica se o produto existe
                if (!await db.Produtos.AnyAsync(p => p.NomeProduto == dto.NomeProdutoPedido))
                    return Results.NotFound();

                ItemComanda item = new ItemComanda
                {
                    ComandaId = dto.ComandaId,
                    NomeProdutoPedido = dto.NomeProdutoPedido,
                    QuantidadeProduto = dto.QuantidadeProduto
                };

                db.ItensComanda.Add(item);
                await db.SaveChangesAsync();

                return Results.Created($"/item/{item.Id}", item);
            });

            //Lista todos os itens cadastrados
            app.MapGet("/item", async (AppDbContext db) =>
            {
                var itens = await db.ItensComanda.ToListAsync();
                return Results.Ok(itens);
            });

            //Atualiza um item da comanda
            app.MapPut("/item/{id}", async (int id, UpdateItemDTO dto, AppDbContext db) =>
            {
                var itemAchado = await db.ItensComanda.FindAsync(id);

                if (itemAchado == null)
                    return Results.NotFound();

                itemAchado.NomeProdutoPedido = dto.NomeProdutoPedido;
                itemAchado.QuantidadeProduto = dto.QuantidadeProduto;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            //Remove um item da comanda
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