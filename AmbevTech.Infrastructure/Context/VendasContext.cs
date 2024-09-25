using AmbevTech.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AmbevTech.Infrastructure.Context
{
    public class VendasContext : DbContext
    {
        public VendasContext(DbContextOptions<VendasContext> options) : base(options) { }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Venda>().HasKey(x => x.NumeroVenda);
            modelBuilder.Entity<Venda>().ToTable("Vendas");
            //modelBuilder.Entity<ItemVenda>().ToTable("ItensVenda");
        }
    }
}