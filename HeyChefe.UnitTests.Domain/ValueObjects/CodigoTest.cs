using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Codigo;
using HeyChefe.Domain.Validacoes.Codigo.Mensagens;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class CodigoTest
    {
        [Fact]
        public void Create_ComValorValido_DeveCriarCodigoComSucesso()
        {
            // Arrange & Act
            var codigo = Codigo.Create(1);

            // Assert
            Assert.NotNull(codigo);
            Assert.Equal(1, codigo.Valor);
        }

        [Fact]
        public void Create_ComValorZero_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Codigo.Create(0));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CodigoValidacao>(exception);
            Assert.Equal(MensagensCodigo.CODIGO_MENOR_IGUAL_ZERO, exception.Message);
        }

        [Fact]
        public void Create_ComValorNegativo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Codigo.Create(-5));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CodigoValidacao>(exception);
            Assert.Equal(MensagensCodigo.CODIGO_MENOR_IGUAL_ZERO, exception.Message);
        }

        [Fact]
        public void InstanciasDistintas_ComMesmoValor_DeveCompararPorReferencia()
        {
            // Arrange & Act
            var codigo1 = Codigo.Create(1);
            var codigo2 = Codigo.Create(1);

            // Assert
            // Codigo é uma classe sem override de Equals, portanto a igualdade é por referência.
            Assert.NotSame(codigo1, codigo2);
            Assert.False(codigo1.Equals(codigo2));
        }
    }
}
