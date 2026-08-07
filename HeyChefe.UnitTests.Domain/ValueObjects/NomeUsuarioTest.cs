using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class NomeUsuarioTest
    {
        [Fact]
        public void Create_ComNomesValidos_DeveCriarNomeComSucesso()
        {
            // Arrange & Act
            var nome = NomeUsuario.Create("Carlos", "Silva");

            // Assert
            Assert.NotNull(nome);
            Assert.Equal("Carlos", nome.Primeiro);
            Assert.Equal("Silva", nome.Segundo);
            Assert.Equal("Carlos Silva", nome.Completo);
        }

        [Fact]
        public void Create_ComEspacosAoRedor_DeveRemoverEspacos()
        {
            // Arrange & Act
            var nome = NomeUsuario.Create("  Carlos  ", "  Silva  ");

            // Assert
            Assert.Equal("Carlos", nome.Primeiro);
            Assert.Equal("Silva", nome.Segundo);
        }

        [Fact]
        public void Create_ComPrimeiroNomeNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => NomeUsuario.Create(null!, "Silva"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensUsuarios.NOME_NULO, exception.Message);
        }

        [Fact]
        public void Create_ComSegundoNomeVazio_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => NomeUsuario.Create("Carlos", ""));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.NOME_MINIMO, exception.Message);
        }

        [Fact]
        public void Create_ComNomeMuitoCurto_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => NomeUsuario.Create("Ca", "Silva"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.NOME_MINIMO, exception.Message);
        }

        [Fact]
        public void Create_ComNomeContendoCaracteresInvalidos_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => NomeUsuario.Create("Carlos1", "Silva"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.NOME_INVALIDO, exception.Message);
        }

        [Fact]
        public void Create_ComNomeMuitoLongo_DeveLancarExcecao()
        {
            // Arrange
            var nomeLongo = new string('A', 101);

            // Act
            var exception = Record.Exception(() => NomeUsuario.Create(nomeLongo, "Silva"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.NOME_MAXIMO, exception.Message);
        }

        [Fact]
        public void InstanciasComMesmosNomes_DeveCompararPorIgualdade()
        {
            // Arrange & Act
            var nome1 = NomeUsuario.Create("Carlos", "Silva");
            var nome2 = NomeUsuario.Create("Carlos", "Silva");

            // Assert
            Assert.Equal(nome1, nome2);
        }
    }
}
