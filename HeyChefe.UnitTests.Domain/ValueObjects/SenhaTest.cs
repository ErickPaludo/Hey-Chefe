using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class SenhaTest
    {
        [Fact]
        public void Create_ComSaltEHashValidos_DeveCriarSenhaComSucesso()
        {
            // Arrange & Act
            var senha = Senha.Create("salt123", "hash123");

            // Assert
            Assert.NotNull(senha);
            Assert.Equal("salt123", senha.Salt);
            Assert.Equal("hash123", senha.Hash);
        }

        [Fact]
        public void Create_ComSaltVazio_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Senha.Create("", "hash123"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.SENHA_VAZIA, exception.Message);
        }

        [Fact]
        public void Create_ComHashVazio_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Senha.Create("salt123", ""));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.SENHA_VAZIA, exception.Message);
        }

        [Fact]
        public void AtualizaSenha_ComSenhaDiferente_DevePermitirSemExcecao()
        {
            // Arrange
            var senha = Senha.Create("salt123", "hash123");
            var novaSenha = Senha.Create("salt456", "hash456");

            // Act
            var exception = Record.Exception(() => senha.AtualizaSenha(novaSenha));

            // Assert
            Assert.Null(exception);
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
        public void AtualizaSenha_ComSenhaIdentica_DeveLancarExcecao()
        {
            // Arrange
            var senha = Senha.Create("salt123", "hash123");
            var senhaIdentica = Senha.Create("salt123", "hash123");

            // Act
            var exception = Record.Exception(() => senha.AtualizaSenha(senhaIdentica));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.MESMA_SENHA, exception.Message);
        }

        [Fact]
        public void InstanciasComMesmosValores_DeveCompararPorIgualdade()
        {
            // Arrange & Act
            var senha1 = Senha.Create("salt123", "hash123");
            var senha2 = Senha.Create("salt123", "hash123");

            // Assert
            Assert.Equal(senha1, senha2);
        }
    }
}
