using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Entidades.Mesas;
using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Entidades.Segurança;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Infra.Data.ConfigTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Infra.Data.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Autenticacao> Autenticacao { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<LinhaPedido> LinhasPedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
