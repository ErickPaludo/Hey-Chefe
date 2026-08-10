using HeyChefe.Domain.Entidades.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Infra.Data.ConfigTable
{
    public class LinhasPedidosConfig : IEntityTypeConfiguration<LinhaPedido>
    {
        public void Configure(EntityTypeBuilder<LinhaPedido> builder)
        {
            builder.ToTable("tb_linhas_pedidos");

            builder.HasKey(p => p.Id);

            builder.Property(u => u.Situacao)
                   .HasComment("Situação do usuário: 0-Pendente | 1-Pronto | 2-Concluido |3-Cancelado")
                   .IsRequired();

            #region Chave Estrageira
            //builder.HasOne(i => i.Pedido)
            //    .WithMany()
            //    .HasForeignKey("PedidoId")
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Item)
                .WithMany()
                .HasForeignKey("ItemId")
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
