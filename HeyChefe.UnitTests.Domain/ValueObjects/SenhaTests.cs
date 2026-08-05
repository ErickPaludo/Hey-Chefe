using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class SenhaTests
    {
        [Fact]
        public void Create_ComDadosValidos_DeveCriarSenhaComSucesso()
        {
            // Arrange
            string salt = "salt123";
            string hash = "hash123";

            // Act
            var senha = Senha.Create(salt, hash);

            // Assert
            Assert.NotNull(senha);
            Assert.Equal(salt, senha.Salt);
            Assert.Equal(hash, senha.Hash);
        }

        [Fact]
        public void Create_ComEspacosNasExtremidades_DeveRemoverEspacos()
        {
            // Arrange
            string salt = "  salt123  ";
            string hash = "  hash123  ";

            // Act
            var senha = Senha.Create(salt, hash);

            // Assert
            Assert.Equal("salt123", senha.Salt);
            Assert.Equal("hash123", senha.Hash);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_ComSaltNuloOuVazio_DeveLancarExcecao(string? saltInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Senha.Create(saltInvalido!, "hash123"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.SENHA_VAZIA, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_ComHashNuloOuVazio_DeveLancarExcecao(string? hashInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Senha.Create("salt123", hashInvalido!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.SENHA_VAZIA, exception.Message);
        }

        [Fact]
        public void AtualizaSenha_ComSenhaNula_DeveLancarExcecao()
        {
            // Arrange
            var senha = Senha.Create("salt123", "hash123");

            // Act
            var exception = Record.Exception(() => senha.AtualizaSenha(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensUsuarios.SENHA_NULA, exception.Message);
        }

        [Fact]
        public void AtualizaSenha_ComMesmoSaltEHash_DeveLancarExcecao()
        {
            // Arrange
            var senhaAtual = Senha.Create("salt123", "hash123");
            var senhaIgual = Senha.Create("salt123", "hash123");

            // Act
            var exception = Record.Exception(() => senhaAtual.AtualizaSenha(senhaIgual));

            // Assert
            // Como Senha é um record, "==" compara por valor (Salt e Hash),
            // então duas instâncias com os mesmos dados são consideradas "a mesma senha".
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.MESMA_SENHA, exception.Message);
        }

        [Fact]
        public void AtualizaSenha_ComSenhaDiferente_NaoDeveLancarExcecao()
        {
            // Arrange
            var senhaAtual = Senha.Create("salt123", "hash123");
            var senhaNova = Senha.Create("saltNovo", "hashNovo");

            // Act
            var exception = Record.Exception(() => senhaAtual.AtualizaSenha(senhaNova));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void AtualizaSenha_ComSenhaDiferente_NaoAlteraOSaltEHashDaInstanciaAtual()
        {
            // Arrange
            var senhaAtual = Senha.Create("salt123", "hash123");
            var senhaNova = Senha.Create("saltNovo", "hashNovo");

            // Act
            senhaAtual.AtualizaSenha(senhaNova);

            // Assert
            // ATENÇÃO: AtualizaSenha só valida (nulo / mesma senha) e não retorna
            // valor algum. Como Salt e Hash não possuem setter público, o método
            // NÃO atualiza de fato os dados da instância atual — quem chama esse
            // método precisa, por conta própria, atribuir uma nova instância de
            // Senha (ex.: no Usuario) para que a troca realmente aconteça.
            Assert.Equal("salt123", senhaAtual.Salt);
            Assert.Equal("hash123", senhaAtual.Hash);
        }

        [Fact]
        public void Equals_ComMesmoSaltEHash_DevemSerIguais()
        {
            // Arrange
            var senha1 = Senha.Create("salt123", "hash123");
            var senha2 = Senha.Create("salt123", "hash123");

            // Act & Assert
            Assert.Equal(senha1, senha2);
            Assert.True(senha1 == senha2);
        }

        [Fact]
        public void Equals_ComSaltOuHashDiferente_NaoDevemSerIguais()
        {
            // Arrange
            var senha1 = Senha.Create("salt123", "hash123");
            var senha2 = Senha.Create("saltDiferente", "hash123");

            // Act & Assert
            Assert.NotEqual(senha1, senha2);
            Assert.False(senha1 == senha2);
        }
    }
}
