using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Entidades.Mesas;
using HeyChefe.Domain.Entidades.Mesas.Enums;
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
        private static Pedido PedidoValido()
        {
            var mesa = Mesa.Create(Codigo.Create(99), ESituacaoMesa.Disponivel);
            return Pedido.Create(
                Codigo.Create(100),
                mesa,
                Usuario.Create(
                    NomeUsuario.Create("Carlos", "Silva"),
                    Email.Create("carlos.silva@email.com"),
                    Senha.Create("salt123", "hash123"),
                    EPermissaoUsuario.Garcom));
        }

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
            var pedido = PedidoValido();
            var item = ItemValido();
            var quantidade = 2;
            var cortesia = false;

            var linha = LinhaPedido.Create(pedido, item, quantidade, cortesia);

            Assert.NotNull(linha);
            Assert.Equal(pedido, linha.Pedido);
            Assert.Equal(item, linha.Item);
            Assert.Equal(quantidade, linha.Quantidade);
            Assert.Equal(cortesia, linha.Cortesia);
            Assert.Equal(ESituacaoLinhaPedido.Pendente, linha.Situacao);
            Assert.Contains(linha, pedido.LinhasPedido);
        }

        [Fact]
        public void Create_ComCortesiaTrue_DeveCriarLinhaComCortesia()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, true);

            Assert.NotNull(linha);
            Assert.True(linha.Cortesia);
        }

        [Fact]
        public void Create_ComPedidoNulo_DeveLancarExcecao()
        {
            var ex = Record.Exception(() => LinhaPedido.Create(null!, ItemValido(), 1, false));

            Assert.NotNull(ex);
            Assert.IsType<ExceptionDomain>(ex);
            Assert.Equal(MensagensPedido.PEDIDO_INVALIDO, ex.Message);
        }

        [Fact]
        public void Create_ComItemNulo_DeveLancarExcecao()
        {
            var ex = Record.Exception(() => LinhaPedido.Create(PedidoValido(), null!, 1, false));

            Assert.NotNull(ex);
            Assert.IsType<ExceptionDomain>(ex);
            Assert.Equal(MensagensPedido.ITEM_INVALIDO, ex.Message);
        }

        [Fact]
        public void Create_ComQuantidadeZero_DeveLancarExcecao()
        {
            var ex = Record.Exception(() => LinhaPedido.Create(PedidoValido(), ItemValido(), 0, false));

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.QUANTIDADE_INVALIDA, ex.Message);
        }

        [Fact]
        public void Create_ComQuantidadeNegativa_DeveLancarExcecao()
        {
            var ex = Record.Exception(() => LinhaPedido.Create(PedidoValido(), ItemValido(), -3, false));

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.QUANTIDADE_INVALIDA, ex.Message);
        }

        [Fact]
        public void Iniciado_ComSituacaoPendente_DeveRetornarFalse()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            Assert.False(linha.Iniciado());
        }

        [Fact]
        public void Iniciado_ComSituacaoCancelado_DeveRetornarFalse()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Cancelado);

            Assert.False(linha.Iniciado());
        }

        [Fact]
        public void Iniciado_ComSituacaoPronto_DeveRetornarTrue()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pronto);

            Assert.True(linha.Iniciado());
        }

        [Fact]
        public void Iniciado_ComSituacaoConcluido_DeveRetornarTrue()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Concluido);

            Assert.True(linha.Iniciado());
        }

        [Fact]
        public void AtualizarQuantidade_ComValorValido_DeveAtualizarQuantidade()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            linha.AtualizarQuantidade(5);

            Assert.Equal(5, linha.Quantidade);
        }

        [Fact]
        public void AtualizarQuantidade_ComValorZero_DeveLancarExcecao()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            var ex = Record.Exception(() => linha.AtualizarQuantidade(0));

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.QUANTIDADE_INVALIDA, ex.Message);
        }

        [Fact]
        public void AtualizarQuantidade_ComValorNegativo_DeveLancarExcecao()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            var ex = Record.Exception(() => linha.AtualizarQuantidade(-2));

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.QUANTIDADE_INVALIDA, ex.Message);
        }

        [Fact]
        public void AtualizarSituacao_ComSituacaoValida_DeveAtualizarSituacao()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pronto);

            Assert.Equal(ESituacaoLinhaPedido.Pronto, linha.Situacao);
        }

        [Fact]
        public void AtualizarSituacao_ComSituacaoInvalida_DeveLancarExcecao()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);
            var situacaoInvalida = (ESituacaoLinhaPedido)999;

            var ex = Record.Exception(() => linha.AtualizarSituacao(situacaoInvalida));

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensBase.SITUACAO_INVALIDA, ex.Message);
        }

        [Fact]
        public void AtualizarCortesia_ComValorValido_DeveAtualizarCortesia()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, false);

            linha.AtualizarCortesia(true);

            Assert.True(linha.Cortesia);
        }

        [Fact]
        public void AtualizarCortesia_ComValorFalso_DeveAtualizarCortesia()
        {
            var linha = LinhaPedido.Create(PedidoValido(), ItemValido(), 1, true);

            linha.AtualizarCortesia(false);

            Assert.False(linha.Cortesia);
        }
    }
}
