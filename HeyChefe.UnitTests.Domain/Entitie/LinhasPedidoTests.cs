using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Objetos_de_Valor.Observação;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes.Pedidos;
using HeyChefe.Domain.Validacoes.Pedidos.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.Entitie
{
    public class LinhasPedidoTests
    {
        private static Usuario UsuarioValido() => Usuario.Create(
            Nome.Create("Carlos", "Silva"),
            Email.Create("carlos.silva@email.com"),
            Senha.Create("salt123", "hash123"),
            EPermissaoUsuario.Garcom);

        private static Pedido PedidoValido() =>
            Pedido.Create(Codigo.Create(1), UsuarioValido());

        private static Item ItemValido() => Item.Create(
            Codigo.Create(10),
            ObservacaoItem.Create("Sem cebola"),
            TituloItem.Create("Pizza"),
            Saldo.Create(50),
            Saldo.Create(10));

        [Fact]
        public void Create_ComDadosValidos_DeveCriarLinhaPedidoComSucesso()
        {
            // Arrange
            var pedido = PedidoValido();
            var item = ItemValido();
            int quantidade = 3;

            // Act
            var linhaPedido = LinhasPedido.Create(pedido, item, quantidade);

            // Assert
            Assert.NotNull(linhaPedido);
            Assert.Equal(pedido, linhaPedido.Pedido);
            Assert.Equal(item, linhaPedido.Item);
            Assert.Equal(quantidade, linhaPedido.Quantidade);
            Assert.Equal(ESituacaoLinhaPedido.Pendente, linhaPedido.Situacao);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Create_ComQuantidadeMenorOuIgualAZero_DeveLancarExcecao(int quantidadeInvalida)
        {
            // Arrange & Act
            var exception = Record.Exception(() => LinhasPedido.Create(PedidoValido(), ItemValido(), quantidadeInvalida));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.QUANTIDADE_INVALIDA, exception.Message);
        }

        [Fact]
        public void Create_ComQuantidadeMinimaValida_DeveCriarComSucesso()
        {
            // Arrange & Act
            var linhaPedido = LinhasPedido.Create(PedidoValido(), ItemValido(), 1);

            // Assert
            Assert.Equal(1, linhaPedido.Quantidade);
        }

        [Fact]
        public void AtualizarQuantidade_ComValorValido_DeveAtualizarQuantidade()
        {
            // Arrange
            var linhaPedido = LinhasPedido.Create(PedidoValido(), ItemValido(), 2);

            // Act
            linhaPedido.AtualizarQuantidade(5);

            // Assert
            Assert.Equal(5, linhaPedido.Quantidade);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void AtualizarQuantidade_ComValorInvalido_DeveLancarExcecao(int quantidadeInvalida)
        {
            // Arrange
            var linhaPedido = LinhasPedido.Create(PedidoValido(), ItemValido(), 2);

            // Act
            var exception = Record.Exception(() => linhaPedido.AtualizarQuantidade(quantidadeInvalida));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.QUANTIDADE_INVALIDA, exception.Message);
            // Garante que o estado anterior não foi alterado
            Assert.Equal(2, linhaPedido.Quantidade);
        }

        [Theory]
        [InlineData(ESituacaoLinhaPedido.Concluido)]
        [InlineData(ESituacaoLinhaPedido.Cancelado)]
        [InlineData(ESituacaoLinhaPedido.Pendente)]
        public void AtualizarSituacao_ComValorValido_DeveAtualizarSituacao(ESituacaoLinhaPedido situacaoValida)
        {
            // Arrange
            var linhaPedido = LinhasPedido.Create(PedidoValido(), ItemValido(), 2);

            // Act
            linhaPedido.AtualizarSituacao(situacaoValida);

            // Assert
            Assert.Equal(situacaoValida, linhaPedido.Situacao);
        }

        [Fact]
        public void AtualizarSituacao_ComValorInvalido_DeveLancarExcecao()
        {
            // Arrange
            var linhaPedido = LinhasPedido.Create(PedidoValido(), ItemValido(), 2);
            var situacaoInvalida = (ESituacaoLinhaPedido)999;

            // Act
            var exception = Record.Exception(() => linhaPedido.AtualizarSituacao(situacaoInvalida));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            // Garante que o estado anterior não foi alterado
            Assert.Equal(ESituacaoLinhaPedido.Pendente, linhaPedido.Situacao);
        }
    }
}
