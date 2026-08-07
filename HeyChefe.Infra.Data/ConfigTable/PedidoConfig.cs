using HeyChefe.Domain.Entidades.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Infra.Data.ConfigTable
{
    public class PedidoConfig : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("tb_pedidos");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(i => i.NumeroPedido,
               numeroPedido =>
               {
                   numeroPedido.Property(x => x.Valor)
                  .HasColumnName("NumeroPedido")
                  .IsRequired();

                   numeroPedido.HasIndex(i => i.Valor)
                       .IsUnique();
               }
            );

            builder.Property(p => p.Prioridade)
                .HasColumnName("Prioridade")
                .IsRequired();

            builder.Property(u => u.Situacao)
                .HasComment("Situação do usuário: 0-Pendente | 1-Iniciado | 2-Concluido |3-Cancelado")
                .IsRequired();

            #region Chave Estrageira
            builder.HasOne(i => i.Usuario)
                .WithMany()
                .HasForeignKey("UsuarioId")
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
