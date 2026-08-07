using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Item.Mensagens;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class SaldoTest
    {
        [Fact]
        public void Create_ComValorPositivo_DeveCriarSaldoComSucesso()
        {
            // Arrange & Act
            var saldo = Saldo.Create(25m);

            // Assert
            Assert.NotNull(saldo);
            Assert.Equal(25m, saldo.Valor);
        }

        [Fact]
        public void Create_ComValorZero_DeveCriarSaldoComSucesso()
        {
            // Arrange & Act
            var saldo = Saldo.Create(0m);

            // Assert
            Assert.NotNull(saldo);
            Assert.Equal(0m, saldo.Valor);
        }

        [Fact]
        public void Create_ComValorNegativo_DeveCriarSaldoComSucesso()
        {
            // Arrange & Act
            var saldo = Saldo.Create(-5m);

            // Assert
            Assert.NotNull(saldo);
            Assert.Equal(-5m, saldo.Valor);
        }

        [Fact]
        public void Soma_ComValoresValidos_DeveRetornarSoma()
        {
            // Arrange
            var saldo = Saldo.Create(10m);

            // Act
            var resultado = saldo.Soma(Saldo.Create(5m));

            // Assert
            Assert.Equal(15m, resultado.Valor);
        }

        [Fact]
        public void Soma_ComSaldoNulo_DeveLancarExcecao()
        {
            // Arrange
            var saldo = Saldo.Create(10m);

            // Act
            var exception = Record.Exception(() => saldo.Soma(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagemItem.VALOR_NULO, exception.Message);
        }

        [Fact]
        public void Subtrai_ComValoresValidos_DeveRetornarSubtracao()
        {
            // Arrange
            var saldo = Saldo.Create(10m);

            // Act
            var resultado = saldo.Subtrai(Saldo.Create(3m));

            // Assert
            Assert.Equal(7m, resultado.Valor);
        }

        [Fact]
        public void Subtrai_ComSaldoNulo_DeveLancarExcecao()
        {
            // Arrange
            var saldo = Saldo.Create(10m);

            // Act
            var exception = Record.Exception(() => saldo.Subtrai(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagemItem.VALOR_NULO, exception.Message);
        }

        [Fact]
        public void Porcentagem_ComMargemDeDez_DeveCalcularAcrescimo()
        {
            // Arrange
            var saldo = Saldo.Create(100m);

            // Act
            var resultado = saldo.Porcentagem(Saldo.Create(10m));

            // Assert
            Assert.Equal(100.1m, resultado.Valor);
        }

        [Fact]
        public void Porcentagem_ComMargemZero_DeveRetornarMesmoValor()
        {
            // Arrange
            var saldo = Saldo.Create(100m);

            // Act
            var resultado = saldo.Porcentagem(Saldo.Create(0m));

            // Assert
            Assert.Equal(100m, resultado.Valor);
        }

        [Fact]
        public void Porcentagem_ComSaldoNulo_DeveLancarExcecao()
        {
            // Arrange
            var saldo = Saldo.Create(100m);

            // Act
            var exception = Record.Exception(() => saldo.Porcentagem(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagemItem.VALOR_NULO, exception.Message);
        }

        [Fact]
        public void InstanciasComMesmoValor_DeveCompararPorIgualdade()
        {
            // Arrange & Act
            var saldo1 = Saldo.Create(10m);
            var saldo2 = Saldo.Create(10m);

            // Assert
            Assert.Equal(saldo1, saldo2);
        }
    }
}
