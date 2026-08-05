using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.Entitie
{
    public class UsuarioTests
    {
        [Fact]
        public void Deve_Criar_Usuario_Com_Sucesso()
        {
            // Arrange
            string nomeCompleto = "Carlos Silva";
            string login = "carlos.silva";
            string senha = "senhaSegura123";
            EPermissaoUsuario perfil = EPermissaoUsuario.Garcom;

            // Act
            var usuario = new Usuario(nomeCompleto, login, senha, perfil);

            // Assert
            Assert.Equal(nomeCompleto, usuario.NomeCompleto);
            Assert.Equal(login, usuario.Login);
            Assert.Equal(senha, usuario.Senha);
            Assert.Equal(perfil, usuario.PerfilAcesso);
            // O status inicial deve ser Ativo
            Assert.Equal(ESituacaoUsuario.Ativo, usuario.Status);
        }

        [Fact]
        public void Deve_Alterar_Status_Para_Inativo()
        {
            // Arrange
            var usuario = new Usuario("Ana Souza", "ana.admin", "admin123", EPermissaoUsuario.Administrador);

            // Act
            usuario.AlterarStatus(ESituacaoUsuario.Inativo);

            // Assert
            Assert.Equal(ESituacaoUsuario.Inativo, usuario.Status);
        }

        [Fact]
        public void Deve_Alterar_Dados_Do_Usuario()
        {
            // Arrange
            var usuario = new Usuario("João", "joao.garcom", "senha123", EPermissaoUsuario.Garcom);
            string novoNome = "João Pedro";
            string novaSenha = "novaSenha321";

            // Act
            usuario.AlterarDados(novoNome, novaSenha);

            // Assert
            Assert.Equal(novoNome, usuario.NomeCompleto);
            Assert.Equal(novaSenha, usuario.Senha);
        }
    }
}
