using Controlador_de_comandas.Data;
using Controlador_de_comandas.Models;
using Microsoft.EntityFrameworkCore;

namespace Controlador_de_comandas.EndPoints
{
    public static class ProdutoEndPoints
    {
        public static void MapProdutoEndPoints(this WebApplication app)
        {
            app.MapPost("/produto", async (Produto produto, AppDbContext db) =>
            {
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();

                return Results.Created($"/produtos/{produto.Id}", produto);
            });

            app.MapGet("/produto", async (AppDbContext db) =>
            {
                var produtos = await db.Produtos.ToListAsync();
                return Results.Ok(produtos);
            });

            app.MapGet("/produto/{id}", async (int id, AppDbContext db) =>
            {
                var produtoAchado = await db.Produtos.FindAsync(id);

                if (produtoAchado == null)
                    return Results.NotFound();

                return Results.Ok(produtoAchado);
            });

            app.MapPut("/produto/{id}", async (int id, Produto produtoNovo, AppDbContext db) =>
            {
                var produtoAchado = await db.Produtos.FindAsync(id);

                if (produtoAchado == null)
                    return Results.NotFound();

                produtoAchado.NomeProduto = produtoNovo.NomeProduto;
                produtoAchado.PrecoProduto = produtoNovo.PrecoProduto;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/produto/{id}", async (int id, AppDbContext db) =>
            {
                var produtoAchado = await db.Produtos.FindAsync(id);

                if (produtoAchado == null)
                    return Results.NotFound();

                db.Produtos.Remove(produtoAchado);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
