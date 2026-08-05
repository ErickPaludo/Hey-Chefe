using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class SaldoTests
    {
        [Theory]
        [InlineData(100)]
        [InlineData(0.5)]
        [InlineData(0)]
        [InlineData(-50)]
        public void Create_ComQualquerValorDecimal_DeveCriarSaldoComSucesso(decimal valor)
        {
            // Arrange & Act
            // Observação: ValidaValor delega para ValidaNulo.Verifica, que recebe um
            // "object?" — como decimal é um tipo de valor, o boxing nunca resulta em
            // null, então nenhuma exceção é lançada aqui independente do valor informado
            // (incluindo negativos), mesmo o método se chamando "ValidaValor".
            var saldo = Saldo.Create(valor);

            // Assert
            Assert.NotNull(saldo);
            Assert.Equal(valor, saldo.Valor);
        }

        [Fact]
        public void Soma_ComSaldoValido_DeveRetornarNovoSaldoComAValorSomado()
        {
            // Arrange
            var saldo1 = Saldo.Create(100);
            var saldo2 = Saldo.Create(50);

            // Act
            var resultado = saldo1.Soma(saldo2);

            // Assert
            Assert.Equal(150, resultado.Valor);
        }

        [Fact]
        public void Soma_ComSaldoValido_NaoDeveAlterarOSaldoOriginal()
        {
            // Arrange
            var saldo1 = Saldo.Create(100);
            var saldo2 = Saldo.Create(50);

            // Act
            saldo1.Soma(saldo2);

            // Assert
            Assert.Equal(100, saldo1.Valor);
        }

        [Fact]
        public void Soma_ComSaldoNulo_DeveLancarExcecao()
        {
            // Arrange
            var saldo = Saldo.Create(100);

            // Act
            var exception = Record.Exception(() => saldo.Soma(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
        }

        [Fact]
        public void Subtrai_ComSaldoValido_DeveRetornarNovoSaldoComAValorSubtraido()
        {
            // Arrange
            var saldo1 = Saldo.Create(100);
            var saldo2 = Saldo.Create(30);

            // Act
            var resultado = saldo1.Subtrai(saldo2);

            // Assert
            Assert.Equal(70, resultado.Valor);
        }

        [Fact]
        public void Subtrai_ComValorMaiorQueOSaldo_DevePermitirSaldoNegativo()
        {
            // Arrange
            var saldo1 = Saldo.Create(30);
            var saldo2 = Saldo.Create(100);

            // Act
            var resultado = saldo1.Subtrai(saldo2);

            // Assert
            // Não há validação de saldo mínimo/negativo na implementação atual.
            Assert.Equal(-70, resultado.Valor);
        }

        [Fact]
        public void Subtrai_ComSaldoNulo_DeveLancarExcecao()
        {
            // Arrange
            var saldo = Saldo.Create(100);

            // Act
            var exception = Record.Exception(() => saldo.Subtrai(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
        }

        [Fact]
        public void Porcentagem_ComSaldoValido_DeveSomarValorDivididoPorCem()
        {
            // Arrange
            var saldo1 = Saldo.Create(100);
            var saldo2 = Saldo.Create(20);

            // Act
            var resultado = saldo1.Porcentagem(saldo2);

            // Assert
            // A implementação realiza Valor + (saldo.Valor / 100), e não um cálculo
            // percentual de fato (ex.: Valor * (saldo.Valor / 100)).
            Assert.Equal(100.2m, resultado.Valor);
        }

        [Fact]
        public void Porcentagem_ComSaldoNulo_DeveLancarExcecao()
        {
            // Arrange
            var saldo = Saldo.Create(100);

            // Act
            var exception = Record.Exception(() => saldo.Porcentagem(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
        }

        [Fact]
        public void Equals_ComMesmoValor_DevemSerIguais()
        {
            // Arrange
            var saldo1 = Saldo.Create(100);
            var saldo2 = Saldo.Create(100);

            // Act & Assert
            Assert.Equal(saldo1, saldo2);
            Assert.True(saldo1 == saldo2);
        }

        [Fact]
        public void Equals_ComValoresDiferentes_NaoDevemSerIguais()
        {
            // Arrange
            var saldo1 = Saldo.Create(100);
            var saldo2 = Saldo.Create(200);

            // Act & Assert
            Assert.NotEqual(saldo1, saldo2);
            Assert.False(saldo1 == saldo2);
        }
    }
}
