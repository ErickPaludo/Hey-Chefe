using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Codigo;
using HeyChefe.Domain.Validacoes.Codigo.Mensagens;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class CodigoTests
    {
        [Fact]
        public void Create_ComValorValido_DeveCriarCodigo()
        {
            // Arrange
            int valorValido = 10;

            // Act
            var codigo = Codigo.Create(valorValido);

            // Assert
            Assert.NotNull(codigo);
            Assert.Equal(valorValido, codigo.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Create_ComValorInvalido_DeveLancarExcecao(int valorInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Codigo.Create(valorInvalido));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CodigoValidacao>(exception);
            Assert.Equal(MensagensCodigo.CODIGO_MENOR_IGUAL_ZERO, exception.Message);
        }
    }
}
