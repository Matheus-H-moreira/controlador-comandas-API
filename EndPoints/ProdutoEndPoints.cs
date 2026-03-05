using Controlador_de_comandas.Data;
using Controlador_de_comandas.Models;
using Microsoft.EntityFrameworkCore;
using Controlador_de_comandas.DTOs.ProdutoDTOs;

namespace Controlador_de_comandas.EndPoints
{
    public static class ProdutoEndPoints
    {
        public static void MapProdutoEndPoints(this WebApplication app)
        {
            //Cria um novo produto
            app.MapPost("/produtos", async (CreateProdutoDTO dto, AppDbContext db) =>
            {
                var produto = new Produto
                {
                    NomeProduto = dto.NomeProduto,
                    PrecoProduto = dto.PrecoProduto
                };

                db.Produtos.Add(produto);
                await db.SaveChangesAsync();

                return Results.Created($"/produtos/{produto.Id}", produto);
            });

            //Lista todos os produtos cadastrados
            app.MapGet("/produtos", async (AppDbContext db) =>
            {
                var produtos = await db.Produtos.ToListAsync();
                return Results.Ok(produtos);
            });

            //Busca um produto específico pelo Id
            app.MapGet("/produtos/{nome}", async (string nome, AppDbContext db) =>
            {
                var produtoAchado = await db.Produtos.FirstOrDefaultAsync(p => p.NomeProduto == nome);

                if (produtoAchado == null)
                    return Results.NotFound();

                return Results.Ok(produtoAchado);
            });

            //Atualiza o nome de um produto
            app.MapPatch("/produtos/nome/{id}", async (int id, UpdateNomeProdutoDTO dto, AppDbContext db) =>
            {
                var produtoAchado = await db.Produtos.FindAsync(id);

                if (produtoAchado == null)
                    return Results.NotFound();

                produtoAchado.NomeProduto = dto.NomeProduto;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            //Atualiza o preço de um produto
            app.MapPatch("/produtos/preco/{id}", async (int id, UpdatePrecoProdutoDTO dto, AppDbContext db) =>
            {
                var produtoAchado = await db.Produtos.FindAsync(id);

                if (produtoAchado == null)
                    return Results.NotFound();

                produtoAchado.PrecoProduto = dto.PrecoProduto;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            //Remove um produto do sistema
            app.MapDelete("/produtos/{id}", async (int id, AppDbContext db) =>
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
