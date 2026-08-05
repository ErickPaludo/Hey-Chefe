using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Categorias;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class TituloCategoriaTests
    {
        [Fact]
        public void Create_ComTextoValido_DeveCriarTituloComSucesso()
        {
            // Arrange
            string texto = "bebidas";

            // Act
            var titulo = TituloCategoria.Create(texto);

            // Assert
            Assert.NotNull(titulo);
            Assert.Equal("Bebidas", titulo.Texto);
        }

        [Fact]
        public void Create_ComTextoEmMaiusculo_DeveConverterParaTitleCase()
        {
            // Arrange
            string texto = "PRATOS PRINCIPAIS";

            // Act
            var titulo = TituloCategoria.Create(texto);

            // Assert
            Assert.Equal("Pratos Principais", titulo.Texto);
        }

        [Fact]
        public void Create_ComEspacosNasExtremidades_DeveRemoverEspacos()
        {
            // Arrange
            string texto = "  bebidas  ";

            // Act
            var titulo = TituloCategoria.Create(texto);

            // Assert
            Assert.Equal("Bebidas", titulo.Texto);
        }

        [Fact]
        public void Create_ComTextoNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => TituloCategoria.Create(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.TITULO_NULO, exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_ComTextoVazioOuEmBranco_DeveLancarExcecao(string textoInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => TituloCategoria.Create(textoInvalido));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CategoriaValidacao>(exception);
            Assert.Equal(MensagensBase.TITULO_TAMANHO_INVALIDO(2, 30), exception.Message);
        }

        [Fact]
        public void Create_ComTextoMenorQueOMinimo_DeveLancarExcecao()
        {
            // Arrange
            string textoCurto = "a";

            // Act
            var exception = Record.Exception(() => TituloCategoria.Create(textoCurto));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CategoriaValidacao>(exception);
            Assert.Equal(MensagensBase.TITULO_TAMANHO_INVALIDO(2, 30), exception.Message);
        }

        [Fact]
        public void Create_ComTextoMaiorQueOMaximo_DeveLancarExcecao()
        {
            // Arrange
            string textoLongo = new string('a', 31);

            // Act
            var exception = Record.Exception(() => TituloCategoria.Create(textoLongo));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CategoriaValidacao>(exception);
            Assert.Equal(MensagensBase.TITULO_TAMANHO_INVALIDO(2, 30), exception.Message);
        }

        [Fact]
        public void Create_ComTextoNoTamanhoMinimoPermitido_DeveCriarComSucesso()
        {
            // Arrange
            string textoLimite = "aa"; // 2 caracteres

            // Act
            var titulo = TituloCategoria.Create(textoLimite);

            // Assert
            Assert.Equal(2, titulo.Texto.Length);
        }

        [Fact]
        public void Create_ComTextoNoTamanhoMaximoPermitido_DeveCriarComSucesso()
        {
            // Arrange
            string textoLimite = new string('a', 30); // 30 caracteres

            // Act
            var titulo = TituloCategoria.Create(textoLimite);

            // Assert
            Assert.Equal(30, titulo.Texto.Length);
        }

        [Fact]
        public void Equals_ComMesmoTexto_DevemSerIguais()
        {
            // Arrange
            var titulo1 = TituloCategoria.Create("bebidas");
            var titulo2 = TituloCategoria.Create("bebidas");

            // Act & Assert
            Assert.Equal(titulo1, titulo2);
            Assert.True(titulo1 == titulo2);
        }

        [Fact]
        public void Equals_ComTextosDiferentes_NaoDevemSerIguais()
        {
            // Arrange
            var titulo1 = TituloCategoria.Create("bebidas");
            var titulo2 = TituloCategoria.Create("sobremesas");

            // Act & Assert
            Assert.NotEqual(titulo1, titulo2);
            Assert.False(titulo1 == titulo2);
        }
    }
}
