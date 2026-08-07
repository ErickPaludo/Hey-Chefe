using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Infra.Data.ConfigTable
{
    public class CategoriaConfig : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("tb_categorias");

            builder.HasKey(c => c.Id);

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

            builder.OwnsOne(c => c.Titulo,
                titulo =>
                {
                    titulo.Property(c => c.Texto)
                    .HasColumnName("Titulo")
                    .HasMaxLength(TituloCategoria.MaxLenght)
                    .IsRequired();
                }
            );

            builder.OwnsOne(c => c.Cor,
                cor =>
                {
                    cor.Property(c => c.Valor)
                    .HasColumnName("Cor")
                    .IsRequired();
                }
            );
        }
    }
}
