using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Cor;
using HeyChefe.Domain.Validacoes.Cor.Mensagens;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class CorTest
    {
        [Fact]
        public void Create_ComHexValido_DeveCriarCorComSucesso()
        {
            // Arrange & Act
            var cor = Cor.Create("#FFFFFF");

            // Assert
            Assert.NotNull(cor);
            Assert.Equal("#FFFFFF", cor.Valor);
        }

        [Fact]
        public void Create_ComHexMinusculo_DeveCriarCorComSucesso()
        {
            // Arrange & Act
            var cor = Cor.Create("#ff5733");

            // Assert
            Assert.NotNull(cor);
            Assert.Equal("#ff5733", cor.Valor);
        }

        [Fact]
        public void Create_ComValorNulo_DeveCriarCorComValorNulo()
        {
            // Arrange & Act
            var cor = Cor.Create(null!);

            // Assert
            Assert.NotNull(cor);
            Assert.Null(cor.Valor);
        }

        [Fact]
        public void Create_ComValorVazio_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Cor.Create(""));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CorValidacao>(exception);
            Assert.Equal(MensagemCor.COR_INVALIDA, exception.Message);
        }

        [Fact]
        public void Create_ComHexSemHashtag_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Cor.Create("FFFFFF"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CorValidacao>(exception);
            Assert.Equal(MensagemCor.COR_INVALIDA, exception.Message);
        }

        [Fact]
        public void Create_ComHexComTresDigitos_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Cor.Create("#FFF"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CorValidacao>(exception);
            Assert.Equal(MensagemCor.COR_INVALIDA, exception.Message);
        }

        [Fact]
        public void Create_ComCaracteresNaoHexadecimais_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Cor.Create("#ZZZZZZ"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CorValidacao>(exception);
            Assert.Equal(MensagemCor.COR_INVALIDA, exception.Message);
        }

        [Fact]
        public void InstanciasComMesmoValor_DeveCompararPorIgualdade()
        {
            // Arrange & Act
            var cor1 = Cor.Create("#FFFFFF");
            var cor2 = Cor.Create("#FFFFFF");

            // Assert
            Assert.Equal(cor1, cor2);
        }
    }
}
