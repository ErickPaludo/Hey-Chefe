using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Objetos_de_Valor.Observação;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Pedidos;
using HeyChefe.Domain.Validacoes.Pedidos.Mensagens;

namespace HeyChefe.UnitTests.Domain.Entities
{
    public class LinhaPedidoTest
    {
        private static Pedido PedidoValido() => Pedido.Create(
            Codigo.Create(100),
            Usuario.Create(
                NomeUsuario.Create("Carlos", "Silva"),
                Email.Create("carlos.silva@email.com"),
                Senha.Create("salt123", "hash123"),
                EPermissaoUsuario.Garcom));

        private static Item ItemValido() => Item.Create(
            Codigo.Create(10),
            ObservacaoItem.Create("Sem cebola"),
            TituloItem.Create("X-Burger"),
            Saldo.Create(25m),
            Saldo.Create(10m),
            null);

        [Fact]
        public void Create_ComDadosValidos_DeveCriarLinhaPedidoComSucesso()
        {
            // Arrange
            var pedido = PedidoValido();
            var item = ItemValido();
            var quantidade = 2;
            var cortesia = false;

            // Act
            var linha = LinhaPedido.Create(pedido, item, quantidade, cortesia);

            // Assert
            Assert.NotNull(linha);
            Assert.Equal(pedido, linha.Pedido);
            Assert.Equal(item, linha.Item);
            Assert.Equal(quantidade, linha.Quantidade);
            Assert.Equal(cortesia, linha.Cortesia);
            Assert.Equal(ESituacaoLinhaPedido.Pendente, linha.Situacao);
        }

        [Fact]
        public void Create_ComCortesiaTrue_DeveCriarLinhaComCortesia()
        {
            // Arrange & Act
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, true);

            // Assert
            Assert.NotNull(linha);
            Assert.True(linha.Cortesia);
        }

        [Fact]
        public void Create_ComPedidoNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => LinhaPedido.Create(null!, ItemValido(), 1, false));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensPedido.PEDIDO_INVALIDO, exception.Message);
        }

        [Fact]
        public void Create_ComItemNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => LinhaPedido.Create(PedidoValido(), null!, 1, false));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensPedido.ITEM_INVALIDO, exception.Message);
        }

        [Fact]
        public void Create_ComQuantidadeZero_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => LinhaPedido.Create(PedidoValido(), ItemValido(), 0, false));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.QUANTIDADE_INVALIDA, exception.Message);
        }

        [Fact]
        public void Create_ComQuantidadeNegativa_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => LinhaPedido.Create(PedidoValido(), ItemValido(), -3, false));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.QUANTIDADE_INVALIDA, exception.Message);
        }

        [Fact]
        public void Iniciado_ComSituacaoPendente_DeveRetornarFalse()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            // Act
            var iniciado = linha.Iniciado();

            // Assert
            Assert.False(iniciado);
        }

        [Fact]
        public void Iniciado_ComSituacaoCancelado_DeveRetornarFalse()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Cancelado);

            // Act
            var iniciado = linha.Iniciado();

            // Assert
            Assert.False(iniciado);
        }

        [Fact]
        public void Iniciado_ComSituacaoPronto_DeveRetornarTrue()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pronto);

            // Act
            var iniciado = linha.Iniciado();

            // Assert
            Assert.True(iniciado);
        }

        [Fact]
        public void Iniciado_ComSituacaoConcluido_DeveRetornarTrue()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Concluido);

            // Act
            var iniciado = linha.Iniciado();

            // Assert
            Assert.True(iniciado);
        }

        [Fact]
        public void AtualizarQuantidade_ComValorValido_DeveAtualizarQuantidade()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            // Act
            linha.AtualizarQuantidade(5);

            // Assert
            Assert.Equal(5, linha.Quantidade);
        }

        [Fact]
        public void AtualizarQuantidade_ComValorZero_DeveLancarExcecao()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            // Act
            var exception = Record.Exception(() => linha.AtualizarQuantidade(0));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.QUANTIDADE_INVALIDA, exception.Message);
        }

        [Fact]
        public void AtualizarQuantidade_ComValorNegativo_DeveLancarExcecao()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            // Act
            var exception = Record.Exception(() => linha.AtualizarQuantidade(-2));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.QUANTIDADE_INVALIDA, exception.Message);
        }

        [Fact]
        public void AtualizarSituacao_ComSituacaoValida_DeveAtualizarSituacao()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            // Act
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pronto);

            // Assert
            Assert.Equal(ESituacaoLinhaPedido.Pronto, linha.Situacao);
        }

        [Fact]
        public void AtualizarSituacao_ComSituacaoInvalida_DeveLancarExcecao()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);
            var situacaoInvalida = (ESituacaoLinhaPedido)999;

            // Act
            var exception = Record.Exception(() => linha.AtualizarSituacao(situacaoInvalida));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensBase.SITUACAO_INVALIDA, exception.Message);
        }

        [Fact]
        public void AtualizarCortesia_ComValorValido_DeveAtualizarCortesia()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            // Act
            linha.AtualizarCortesia(true);

            // Assert
            Assert.True(linha.Cortesia);
        }

        [Fact]
        public void AtualizarCortesia_ComValorFalso_DeveAtualizarCortesia()
        {
            // Arrange
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, true);

            // Act
            linha.AtualizarCortesia(false);

            // Assert
            Assert.False(linha.Cortesia);
        }
    }
}
