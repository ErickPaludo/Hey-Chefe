using HeyChefe.Domain.Entidades.Segurança;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Infra.Data.ConfigTable
{
    public class AutenticacaoConfig : IEntityTypeConfiguration<Autenticacao>
    {
        public void Configure(EntityTypeBuilder<Autenticacao> builder)
        {
            builder.ToTable("tb_autenticacao");

            builder.HasKey(a => a.IdSession);

            builder.HasOne(a => a.Usuario)
                   .WithMany()
                   .HasForeignKey("UsuarioId")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
