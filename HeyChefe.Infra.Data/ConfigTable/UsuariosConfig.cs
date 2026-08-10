using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Objetos_de_Valor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeyChefe.Infra.Data.ConfigTable
{
    public class UsuariosConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("tb_usuarios");
            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.Nome,
                nome =>
                {
                    nome.Property(x => x.Primeiro)
                   .HasColumnName("PrimeiroNome")
                   .HasMaxLength(NomeUsuario.MaxPrimeiro)
                   .IsRequired();

                    nome.Property(x => x.Segundo)
                    .HasColumnName("SegundoNome")
                    .HasMaxLength(NomeUsuario.MaxSegundo)
                    .IsRequired();
                }
            );

            builder.OwnsOne(u => u.Email,
                endereco =>
                {
                    endereco.Property(x => x.Endereco)
                    .HasColumnName("Email")
                    .HasMaxLength(Email.MaxEndereco)
                    .IsRequired();
                }
            );

            builder.OwnsOne(u => u.Senha,
              senha =>
              {
                  senha.Property(x => x.Salt)
                  .HasColumnName("Salt")
                  .IsRequired();

                  senha.Property(x => x.Hash)
                  .HasColumnName("Senha")
                  .IsRequired();
              }
            );

            builder.Property(u => u.Permissao)
                .HasComment("Permissões do usuário: 0-Administrador | 1-Garçom | 2-Cozinha")
                .IsRequired();

            builder.Property(u => u.Situacao)
             .HasComment("Situação do usuário: 0-Ativo | 1-Inativo")
             .IsRequired();
        }
    }
}
