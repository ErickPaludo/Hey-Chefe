using HeyChefe.Domain.Entidades.Mesas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Infra.Data.ConfigTable
{
    public class MesaConfig : IEntityTypeConfiguration<Mesa>
    {
        public void Configure(EntityTypeBuilder<Mesa> builder)
        {
            builder.ToTable("tb_mesa");
            builder.HasKey(m => m.Id);

            builder.OwnsOne(i => i.Codigo,
               codigo =>
               {
                   codigo.Property(x => x.Valor)
                  .HasColumnName("Codigo")
                  .IsRequired();

                   codigo.HasIndex(i => i.Valor)
                       .IsUnique();
               }
            );

            builder.Property(u => u.Situacao)
             .HasComment("Situação do usuário: 0-Disponivel | 1-Ocupada | 2-LimpezaPendente |3-Reservada")
             .IsRequired();
        }
    }
}
