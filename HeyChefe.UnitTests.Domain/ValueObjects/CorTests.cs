using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Cor;
using HeyChefe.Domain.Validacoes.Cor.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class CorTests
    {
        [Theory]
        [InlineData("#FFFFFF")]
        [InlineData("#000000")]
        [InlineData("#123ABC")]
        [InlineData("#abcdef")]
        [InlineData("#a1B2c3")]
        public void Create_ComHexadecimalValido_DeveCriarCorComSucesso(string valorValido)
        {
            // Arrange & Act
            var cor = Cor.Create(valorValido);

            // Assert
            Assert.NotNull(cor);
            Assert.Equal(valorValido, cor.Valor);
        }

        [Fact]
        public void Create_ComValorNulo_NaoDeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Cor.Create(null!));

            // Assert
            // A implementação atual só valida quando o valor não é nulo,
            // portanto nenhuma exceção deve ser lançada nesse cenário.
            Assert.Null(exception);
        }

        [Fact]
        public void Create_ComValorNulo_DeveManterValorComoNulo()
        {
            // Arrange & Act
            var cor = Cor.Create(null!);

            // Assert
            Assert.Null(cor.Valor);
        }

        [Fact]
        public void Create_ComValorVazio_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Cor.Create(string.Empty));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CorValidacao>(exception);
            Assert.Equal(MensagemCor.COR_INVALIDA, exception.Message);
        }

        [Theory]
        [InlineData("FFFFFF")]           // sem o '#'
        [InlineData("#FFFFF")]           // 5 dígitos
        [InlineData("#FFFFFFF")]         // 7 dígitos
        [InlineData("#GGGGGG")]          // caracteres não hexadecimais
        [InlineData("#FFF")]             // formato abreviado não suportado
        [InlineData("##FFFFFF")]         // '#' duplicado
        [InlineData("#FFFFFF ")]         // espaço extra
        [InlineData(" #FFFFFF")]         // espaço extra
        [InlineData("red")]              // nome de cor, não hexadecimal
        public void Create_ComFormatoInvalido_DeveLancarExcecao(string valorInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Cor.Create(valorInvalido));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CorValidacao>(exception);
            Assert.Equal(MensagemCor.COR_INVALIDA, exception.Message);
        }

        [Fact]
        public void Equals_ComMesmoValor_DevemSerIguais()
        {
            // Arrange
            var cor1 = Cor.Create("#FFFFFF");
            var cor2 = Cor.Create("#FFFFFF");

            // Act & Assert
            Assert.Equal(cor1, cor2);
            Assert.True(cor1 == cor2);
        }

        [Fact]
        public void Equals_ComValoresDiferentes_NaoDevemSerIguais()
        {
            // Arrange
            var cor1 = Cor.Create("#FFFFFF");
            var cor2 = Cor.Create("#000000");

            // Act & Assert
            Assert.NotEqual(cor1, cor2);
            Assert.False(cor1 == cor2);
        }
    }
}
