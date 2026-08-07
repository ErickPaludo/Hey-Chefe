using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Objetos_de_Valor.Observação;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeyChefe.Infra.Data.ConfigTable
{
    public class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("tb_itens");
            builder.HasKey(i => i.Id);

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

            builder.OwnsOne(i => i.Nome,
               nome =>
               {
                   nome.Property(x => x.Texto)
                  .HasColumnName("Nome")
                  .HasMaxLength(TituloItem.MaxLenght)
                  .IsRequired();
               }
            );

            builder.OwnsOne(i => i.Descricao,
               descricao =>
               {
                   descricao.Property(x => x.Texto)
                  .HasColumnName("Descricao")
                  .HasMaxLength(ObservacaoBase.MaxLenght);
               }
            );

            builder.OwnsOne(i => i.PrecoVenda,
               precoVenda =>
               {
                   precoVenda.Property(x => x.Valor)
                  .HasColumnName("PrecoVenda")
                  .HasPrecision(18, 2)
                  .IsRequired();
               }
            );

            builder.OwnsOne(i => i.MargemLucro,
               margemLucro =>
               {
                   margemLucro.Property(x => x.Valor)
                  .HasColumnName("MargemLucro")
                  .HasPrecision(18, 2)
                  .IsRequired();
               }
            );

            builder.Property(u => u.Situacao)
            .HasComment("Situação do usuário: 0-Ativo | 1-Inativo | 2-Excluído |3-Sem Estoque")
            .IsRequired();

            builder.Property<Guid?>("CategoriaId"); //propriedade de chave estrangeira para Categoria, oculta.

            #region Chave Estrageira
            builder.HasOne(i => i.Categoria)
                .WithMany()
                .HasForeignKey("CategoriaId")
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
