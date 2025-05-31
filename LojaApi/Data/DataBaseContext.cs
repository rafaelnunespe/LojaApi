using LojaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<PedidoEmbalado> PedidosEmbalados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                 .OwnsOne(p => p.Dimensoes);

            modelBuilder.Entity<Caixa>()
                .OwnsOne(p => p.Dimensoes);

            modelBuilder.Entity<PedidoEmbalado>()
                .OwnsOne(p => p.Caixas);

            modelBuilder.Entity<Pedido>()
            .HasMany(p => p.Produtos)
            .WithOne(p => p.Pedido)
            .HasForeignKey(p => p.PedidoId);
                      

            base.OnModelCreating(modelBuilder);
        }
    }
}
