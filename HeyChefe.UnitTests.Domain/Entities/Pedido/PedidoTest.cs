using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Pedidos;
using HeyChefe.Domain.Validacoes.Pedidos.Mensagens;

namespace HeyChefe.UnitTests.Domain.Entities
{
    public class PedidoTest
    {
        private static Codigo NumeroPedidoValido() => Codigo.Create(100);
        private static Usuario UsuarioValido() => Usuario.Create(
            NomeUsuario.Create("Carlos", "Silva"),
            Email.Create("carlos.silva@email.com"),
            Senha.Create("salt123", "hash123"),
            EPermissaoUsuario.Garcom);

        [Fact]
        public void Create_ComDadosValidos_DeveCriarPedidoComSucesso()
        {
            // Arrange
            var numeroPedido = NumeroPedidoValido();
            var usuario = UsuarioValido();
            var prioridade = 5;

            // Act
            var pedido = Pedido.Create(numeroPedido, usuario, prioridade);

            // Assert
            Assert.NotNull(pedido);
            Assert.Equal(numeroPedido, pedido.NumeroPedido);
            Assert.Equal(usuario, pedido.Usuario);
            Assert.Equal(prioridade, pedido.Prioridade);
            Assert.Equal(ESituacaoPedido.Pendente, pedido.Situacao);
            Assert.Null(pedido.Fechamento);
            Assert.NotNull(pedido.LinhasPedido);
            Assert.Empty(pedido.LinhasPedido);
        }

        [Fact]
        public void Create_SemPrioridadeInformada_DeveCriarPedidoComPrioridadeZero()
        {
            // Arrange
            var numeroPedido = NumeroPedidoValido();
            var usuario = UsuarioValido();

            // Act
            var pedido = Pedido.Create(numeroPedido, usuario);

            // Assert
            Assert.NotNull(pedido);
            Assert.Equal(0, pedido.Prioridade);
            Assert.Equal(ESituacaoPedido.Pendente, pedido.Situacao);
        }

        [Fact]
        public void Create_ComNumeroPedidoNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Pedido.Create(null!, UsuarioValido(), 5));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.CODIGO_OBRIGATORIO, exception.Message);
        }

        [Fact]
        public void Create_ComUsuarioNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Pedido.Create(NumeroPedidoValido(), null!, 5));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.USUARIO_OBRIGATORIO, exception.Message);
        }

        [Fact]
        public void PermiteRemoverPedido_SemLinhas_DeveRetornarTrue()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());

            // Act
            var permiteRemover = pedido.PermiteRemoverPedido();

            // Assert
            Assert.True(permiteRemover);
        }

        [Fact]
        public void SituacaoPendente_ComPedidoPendente_DeveManterSituacaoPendente()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());

            // Act
            pedido.SituacaoPendente();

            // Assert
            Assert.Equal(ESituacaoPedido.Pendente, pedido.Situacao);
            Assert.Null(pedido.Fechamento);
        }

        [Fact]
        public void SituacaoPendente_ComPedidoIniciado_DeveVoltarParaPendente()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());
            pedido.SituacaoIniciado();

            // Act
            pedido.SituacaoPendente();

            // Assert
            Assert.Equal(ESituacaoPedido.Pendente, pedido.Situacao);
            Assert.Null(pedido.Fechamento);
        }

        [Fact]
        public void SituacaoIniciado_ComPedidoPendente_DeveIniciarPedido()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());

            // Act
            pedido.SituacaoIniciado();

            // Assert
            Assert.Equal(ESituacaoPedido.Iniciado, pedido.Situacao);
        }

        [Fact]
        public void SituacaoIniciado_ComPedidoJaIniciado_DeveLancarExcecao()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());
            pedido.SituacaoIniciado();

            // Act
            var exception = Record.Exception(() => pedido.SituacaoIniciado());

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.PEDIDO_JA_INICIADO, exception.Message);
        }

        [Fact]
        public void SituacaoIniciado_ComPedidoCancelado_DeveLancarExcecao()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());
            pedido.SituacaoCancelado();

            // Act
            var exception = Record.Exception(() => pedido.SituacaoIniciado());

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.PEDIDO_JA_INICIADO, exception.Message);
        }

        [Fact]
        public void SituacaoConcluido_ComPedidoPendente_DeveConcluirPedido()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());

            // Act
            pedido.SituacaoConcluido();

            // Assert
            Assert.Equal(ESituacaoPedido.Concluido, pedido.Situacao);
            Assert.NotNull(pedido.Fechamento);
        }

        [Fact]
        public void SituacaoConcluido_ComPedidoCancelado_DeveLancarExcecao()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());
            pedido.SituacaoCancelado();

            // Act
            var exception = Record.Exception(() => pedido.SituacaoConcluido());

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.PEDIDO_CANCELADO, exception.Message);
        }

        [Fact]
        public void SituacaoCancelado_ComPedidoPendente_DeveCancelarPedido()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());

            // Act
            pedido.SituacaoCancelado();

            // Assert
            Assert.Equal(ESituacaoPedido.Cancelado, pedido.Situacao);
            Assert.Null(pedido.Fechamento);
        }

        [Fact]
        public void SituacaoCancelado_ComPedidoIniciado_DeveLancarExcecao()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());
            pedido.SituacaoIniciado();

            // Act
            var exception = Record.Exception(() => pedido.SituacaoCancelado());

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.SITUACAO_INVALIDA, exception.Message);
        }

        [Fact]
        public void SituacaoCancelado_ComPedidoJaCancelado_DeveLancarExcecao()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());
            pedido.SituacaoCancelado();

            // Act
            var exception = Record.Exception(() => pedido.SituacaoCancelado());

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PedidoValidacao>(exception);
            Assert.Equal(MensagensPedido.SITUACAO_INVALIDA, exception.Message);
        }

        [Fact]
        public void AtualizarPrioridade_ComValorValido_DeveAtualizarPrioridade()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());

            // Act
            pedido.AtualizarPrioridade(10);

            // Assert
            Assert.Equal(10, pedido.Prioridade);
        }

        [Fact]
        public void AtualizarPrioridade_ComValorNegativo_DeveAtualizarSemValidacao()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());

            // Act
            pedido.AtualizarPrioridade(-1);

            // Assert
            // A implementação atual não valida a prioridade, positiva ou negativa.
            Assert.Equal(-1, pedido.Prioridade);
        }
    }
}
