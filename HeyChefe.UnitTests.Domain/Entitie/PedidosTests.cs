using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.Entitie
{
    public class PedidoTests
    {
        private static Codigo NumeroPedidoValido() => Codigo.Create(1);

        private static Usuario UsuarioValido() => Usuario.Create(
            Nome.Create("Carlos", "Silva"),
            Email.Create("carlos.silva@email.com"),
            Senha.Create("salt123", "hash123"),
            EPermissaoUsuario.Garcom);

        [Fact]
        public void Create_ComPrioridadeInformada_DeveCriarPedidoComSucesso()
        {
            // Arrange
            var numeroPedido = NumeroPedidoValido();
            var usuario = UsuarioValido();
            int prioridade = 5;

            // Act
            var pedido = Pedido.Create(numeroPedido, usuario, prioridade);

            // Assert
            Assert.NotNull(pedido);
            Assert.Equal(numeroPedido, pedido.NumeroPedido);
            Assert.Equal(usuario, pedido.Usuario);
            Assert.Equal(prioridade, pedido.Prioridade);
            Assert.Equal(ESituacaoPedido.Pendente, pedido.Situacao);
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
            Assert.Equal(0, pedido.Prioridade);
            Assert.Equal(ESituacaoPedido.Pendente, pedido.Situacao);
        }

        [Fact]
        public void AtualizarPrioridade_DeveAtualizarSemValidacao()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());

            // Act
            pedido.AtualizarPrioridade(10);

            // Assert
            Assert.Equal(10, pedido.Prioridade);
        }

        [Fact]
        public void AtualizarPrioridade_ComValorNegativo_DevePermitirPoisNaoHaValidacao()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());

            // Act
            pedido.AtualizarPrioridade(-1);

            // Assert
            // A implementação atual não valida a prioridade, positiva ou negativa.
            Assert.Equal(-1, pedido.Prioridade);
        }

        [Fact]
        public void Usuario_DevePermitirAlteracaoDireta()
        {
            // Arrange
            var pedido = Pedido.Create(NumeroPedidoValido(), UsuarioValido());
            var novoUsuario = UsuarioValido();

            // Act
            // A propriedade Usuario possui setter público (diferente das demais
            // propriedades da entidade), permitindo alteração direta sem validação.
            pedido.Usuario = novoUsuario;

            // Assert
            Assert.Equal(novoUsuario, pedido.Usuario);
        }
    }
}
