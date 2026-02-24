using Controlador_de_comandas.Models;
using Microsoft.EntityFrameworkCore;

namespace Controlador_de_comandas.Data
{
    public class AppDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração do DbContext, que estão definidas no arquivo Program.cs
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        // Definição dos DbSet para cada entidade do modelo, permitindo que o Entity Framework Core crie as tabelas correspondentes no banco de dados
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<ItemComanda> ItensComanda { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        // Configuração das relações entre as entidades usando o Fluent API do Entity Framework Core, garantindo que as chaves estrangeiras e as navegações estejam corretamente definidas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Comanda -> ItemComanda: Uma comanda pode ter muitos itens, e cada item pertence a uma única comanda
            modelBuilder.Entity<Comanda>()
                .HasMany(c => c.Pedidos)
                .WithOne(i => i.Comanda)
                .HasForeignKey(i => i.ComandaId);

            //Mesa -> Comanda: Uma mesa pode ter muitas comandas, e cada comanda pertence a uma única mesa
            modelBuilder.Entity<Mesa>()
                .HasMany(m => m.ListaComanda)
                .WithOne(c => c.Mesa)
                .HasForeignKey(c => c.MesaId);

            //Garantir que  qualquer configuração adicional necessária para as entidades seja aplicada
            base.OnModelCreating(modelBuilder);
        }
    }
}
