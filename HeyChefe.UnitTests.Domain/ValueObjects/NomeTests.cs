using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class NomeTests
    {
        [Fact]
        public void Create_ComDadosValidos_DeveCriarNomeComSucesso()
        {
            // Arrange
            string primeiroNome = "Carlos";
            string segundoNome = "Silva";

            // Act
            var nome = Nome.Create(primeiroNome, segundoNome);

            // Assert
            Assert.NotNull(nome);
            Assert.Equal(primeiroNome, nome.Primeiro);
            Assert.Equal(segundoNome, nome.Segundo);
            Assert.Equal("Carlos Silva", nome.Completo);
        }

        [Fact]
        public void Create_ComEspacosEmBranco_DeveRemoverEspacosDasExtremidades()
        {
            // Arrange
            string primeiroNome = "  Carlos  ";
            string segundoNome = "  Silva  ";

            // Act
            var nome = Nome.Create(primeiroNome, segundoNome);

            // Assert
            Assert.Equal("Carlos", nome.Primeiro);
            Assert.Equal("Silva", nome.Segundo);
        }

        [Fact]
        public void Create_ComNomeCompostoContendoEspacoInterno_DeveCriarComSucesso()
        {
            // Arrange
            string primeiroNome = "Maria Clara";
            string segundoNome = "dos Santos";

            // Act
            var nome = Nome.Create(primeiroNome, segundoNome);

            // Assert
            Assert.Equal(primeiroNome, nome.Primeiro);
            Assert.Equal(segundoNome, nome.Segundo);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_ComPrimeiroNomeNuloOuVazio_DeveLancarExcecao(string? primeiroNomeInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Nome.Create(primeiroNomeInvalido!, "Silva"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.NOME_OBRIGATORIO, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_ComSegundoNomeNuloOuVazio_DeveLancarExcecao(string? segundoNomeInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Nome.Create("Carlos", segundoNomeInvalido!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.NOME_OBRIGATORIO, exception.Message);
        }

        [Theory]
        [InlineData("Carlos123")]
        [InlineData("Carlos!")]
        [InlineData("Carlos@Silva")]
        [InlineData("Carlos_Silva")]
        [InlineData("Carlos-Silva")]
        public void Create_ComPrimeiroNomeContendoCaracteresInvalidos_DeveLancarExcecao(string primeiroNomeInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Nome.Create(primeiroNomeInvalido, "Silva"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.NOME_INVALIDO, exception.Message);
        }

        [Theory]
        [InlineData("Silva123")]
        [InlineData("Silva!")]
        [InlineData("Silva@Souza")]
        public void Create_ComSegundoNomeContendoCaracteresInvalidos_DeveLancarExcecao(string segundoNomeInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Nome.Create("Carlos", segundoNomeInvalido));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.NOME_INVALIDO, exception.Message);
        }

        [Theory]
        [InlineData("Jo")]
        [InlineData("A")]
        public void Create_ComPrimeiroNomeMenorQueOMinimo_DeveLancarExcecao(string primeiroNomeCurto)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Nome.Create(primeiroNomeCurto, "Silva"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.NOME_MINIMO, exception.Message);
        }

        [Fact]
        public void Create_ComPrimeiroNomeMaiorQueOMaximo_DeveLancarExcecao()
        {
            // Arrange
            string primeiroNomeLongo = new string('A', 101);

            // Act
            var exception = Record.Exception(() => Nome.Create(primeiroNomeLongo, "Silva"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.NOME_MAXIMO, exception.Message);
        }

        [Fact]
        public void Create_ComPrimeiroNomeNoTamanhoMinimoPermitido_DeveCriarComSucesso()
        {
            // Arrange
            string primeiroNomeLimite = new string('A', 3);

            // Act
            var nome = Nome.Create(primeiroNomeLimite, "Silva");

            // Assert
            Assert.Equal(primeiroNomeLimite, nome.Primeiro);
        }

        [Fact]
        public void Create_ComPrimeiroNomeNoTamanhoMaximoPermitido_DeveCriarComSucesso()
        {
            // Arrange
            string primeiroNomeLimite = new string('A', 100);

            // Act
            var nome = Nome.Create(primeiroNomeLimite, "Silva");

            // Assert
            Assert.Equal(primeiroNomeLimite, nome.Primeiro);
        }

        [Fact]
        public void Equals_ComMesmosValores_DevemSerIguais()
        {
            // Arrange
            var nome1 = Nome.Create("Carlos", "Silva");
            var nome2 = Nome.Create("Carlos", "Silva");

            // Act & Assert
            Assert.Equal(nome1, nome2);
            Assert.True(nome1 == nome2);
        }

        [Fact]
        public void Equals_ComValoresDiferentes_NaoDevemSerIguais()
        {
            // Arrange
            var nome1 = Nome.Create("Carlos", "Silva");
            var nome2 = Nome.Create("João", "Souza");

            // Act & Assert
            Assert.NotEqual(nome1, nome2);
            Assert.False(nome1 == nome2);
        }
    }
}
