using HeyChefe.Domain.Entidades.Mesas;
using HeyChefe.Domain.Entidades.Mesas.Enums;
using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Mesas;
using HeyChefe.Domain.Validacoes.Mesas.Mensagens;

namespace HeyChefe.UnitTests.Domain.Entities
{
    public class MesaTest
    {
        private static Codigo CodigoValido() => Codigo.Create(1);
        private static Usuario UsuarioValido() => Usuario.Create(
            NomeUsuario.Create("Carlos", "Silva"),
            Email.Create("carlos.silva@email.com"),
            Senha.Create("salt123", "hash123"),
            EPermissaoUsuario.Garcom);

        private static Pedido PedidoValido() => Pedido.Create(Codigo.Create(2), UsuarioValido());

        [Fact]
        public void Criar_ComDadosValidos_DeveCriarMesaComSucesso()
        {
            // Arrange
            var codigo = CodigoValido();
            var situacao = ESituacaoMesa.Disponivel;

            // Act
            var mesa = Mesa.Create(codigo, situacao);

            // Assert
            Assert.NotNull(mesa);
            Assert.Equal(codigo, mesa.Codigo);
            Assert.Equal(situacao, mesa.Situacao);
            Assert.NotNull(mesa.Pedidos);
            Assert.Empty(mesa.Pedidos);
        }

        [Fact]
        public void Criar_ComSituacaoInvalida_DeveLancarExcecao()
        {
            // Arrange
            var situacaoInvalida = (ESituacaoMesa)999;

            // Act
            var exception = Record.Exception(() => Mesa.Create(CodigoValido(), situacaoInvalida));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<MesaValidacao>(exception);
            Assert.Equal(MensagensBase.SITUACAO_INVALIDA, exception.Message);
        }

        [Fact]
        public void OcuparMesa_ComMesaDisponivel_DeveOcuparMesa()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);

            // Act
            mesa.OcuparMesa();

            // Assert
            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
        }

        [Fact]
        public void OcuparMesa_ComMesaOcupada_DeveLancarExcecao()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Ocupada);

            // Act
            var exception = Record.Exception(() => mesa.OcuparMesa());

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<MesaValidacao>(exception);
            Assert.Equal(MensagemMesa.MESA_NAO_DISPONIVEL, exception.Message);
        }

        [Fact]
        public void OcuparMesa_ComMesaLimpezaPendente_DeveLancarExcecao()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.LimpezaPendente);

            // Act
            var exception = Record.Exception(() => mesa.OcuparMesa());

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<MesaValidacao>(exception);
            Assert.Equal(MensagemMesa.MESA_NAO_DISPONIVEL, exception.Message);
        }

        [Fact]
        public void OcuparMesa_ComMesaReservada_DeveLancarExcecao()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Reservada);

            // Act
            var exception = Record.Exception(() => mesa.OcuparMesa());

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<MesaValidacao>(exception);
            Assert.Equal(MensagemMesa.MESA_NAO_DISPONIVEL, exception.Message);
        }

        [Fact]
        public void AdicionarPedido_ComMesaDisponivel_DeveAdicionarPedidoEOcuparMesa()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoValido();

            // Act
            mesa.AdicionarPedido(pedido);

            // Assert
            Assert.Contains(pedido, mesa.Pedidos);
            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
        }

        [Fact]
        public void AdicionarPedido_ComMesaOcupada_DeveAdicionarPedido()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Ocupada);
            var pedido = PedidoValido();

            // Act
            mesa.AdicionarPedido(pedido);

            // Assert
            Assert.Contains(pedido, mesa.Pedidos);
            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
        }

        [Fact]
        public void AdicionarPedido_ComMesaLimpezaPendente_DeveLancarExcecao()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.LimpezaPendente);
            var pedido = PedidoValido();

            // Act
            var exception = Record.Exception(() => mesa.AdicionarPedido(pedido));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<MesaValidacao>(exception);
            Assert.Equal(MensagemMesa.MESA_NAO_DISPONIVEL, exception.Message);
        }

        [Fact]
        public void RemovePedido_ComPedidoPertencendoAMesa_DeveRemoverPedido()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoValido();
            mesa.AdicionarPedido(pedido);

            // Act
            mesa.RemovePedido(pedido);

            // Assert
            Assert.DoesNotContain(pedido, mesa.Pedidos);
            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
        }

        [Fact]
        public void RemovePedido_ComPedidoQueNaoPertenceAMesa_DeveLancarExcecao()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoValido();

            // Act
            var exception = Record.Exception(() => mesa.RemovePedido(pedido));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<MesaValidacao>(exception);
            Assert.Equal(MensagemMesa.PEDIDO_NAO_PERTENCE_A_ESTA_MESA, exception.Message);
        }

        [Fact]
        public void AbandonoDeMesa_ComLimparMesaTrue_DeveCancelarPedidosESituacaoLimpezaPendente()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoValido();
            mesa.AdicionarPedido(pedido);

            // Act
            mesa.AbandonoDeMesa(true);

            // Assert
            Assert.Equal(ESituacaoPedido.Cancelado, pedido.Situacao);
            Assert.Equal(ESituacaoMesa.LimpezaPendente, mesa.Situacao);
        }

        [Fact]
        public void AbandonoDeMesa_ComLimparMesaFalse_DeveCancelarPedidosESituacaoDisponivel()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoValido();
            mesa.AdicionarPedido(pedido);

            // Act
            mesa.AbandonoDeMesa(false);

            // Assert
            Assert.Equal(ESituacaoPedido.Cancelado, pedido.Situacao);
            Assert.Equal(ESituacaoMesa.Disponivel, mesa.Situacao);
        }

        [Fact]
        public void FechamentoDeConta_DeveConcluirPedidosESituacaoLimpezaPendente()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoValido();
            mesa.AdicionarPedido(pedido);

            // Act
            mesa.FechamentoDeConta();

            // Assert
            Assert.Equal(ESituacaoPedido.Concluido, pedido.Situacao);
            Assert.NotNull(pedido.Fechamento);
            Assert.Equal(ESituacaoMesa.LimpezaPendente, mesa.Situacao);
        }

        [Fact]
        public void AtualizarSituacao_ComSituacaoValida_DeveAtualizarSituacao()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);

            // Act
            mesa.AtualizarSituacao(ESituacaoMesa.Reservada);

            // Assert
            Assert.Equal(ESituacaoMesa.Reservada, mesa.Situacao);
        }

        [Fact]
        public void AtualizarSituacao_ComSituacaoInvalida_DeveLancarExcecao()
        {
            // Arrange
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var situacaoInvalida = (ESituacaoMesa)999;

            // Act
            var exception = Record.Exception(() => mesa.AtualizarSituacao(situacaoInvalida));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<MesaValidacao>(exception);
            Assert.Equal(MensagensBase.SITUACAO_INVALIDA, exception.Message);
        }
    }
}
