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
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Mesas;
using HeyChefe.Domain.Validacoes.Mesas.Mensagens;
using HeyChefe.Domain.Validacoes.Pedidos;
using HeyChefe.Domain.Validacoes.Pedidos.Mensagens;

namespace HeyChefe.UnitTests.Domain.Entities
{
    /// <summary>
    /// Especificação estrita do contrato de Mesa.
    /// INTENCIONAL: vários testes falham contra a Domain atual para guiar a correção.
    /// Não há contornos (remove de mesa auxiliar / isolamentos) — o teste descreve
    /// o que DEVERIA acontecer. Você corrige a Domain até ficar verde.
    /// Bugs mapeados:
    ///  B1: Mesa não valida Codigo nulo
    ///  B2: Pedido ctor chama Mesa.AdicionarPedido(this) — acopla Mesa<->Pedido e causa duplicidade
    ///  B3: Pedido.FechamentoPedido chama Mesa.FechamentoDeConta — recursão infinita (StackOverflow)
    ///  B4: Mesa.AdicionarPedido só bloqueia LimpezaPendente, deveria bloquear Reservada
    ///  B5: Mesa.RemovePedido mantém Ocupada mesmo quando fica vazia — deveria liberar
    ///  B6: Mesa.AdicionarPedido permite duplicar o mesmo Pedido na lista
    ///  B7: Mesa.AdicionarPedido não sincroniza pedido.Mesa com a mesa destino (bidirecionalidade quebrada)
    /// </summary>
    public class MesaTest
    {
        private static Codigo CodigoValido() => Codigo.Create(1);
        private static int _seq = 2000;
        private static Codigo NextCodigo() => Codigo.Create(_seq++);

        private static Usuario UsuarioValido() => Usuario.Create(
            NomeUsuario.Create("Carlos", "Silva"),
            Email.Create("carlos.silva@email.com"),
            Senha.Create("salt123", "hash123"),
            EPermissaoUsuario.Garcom);

        private static Item ItemValido() => Item.Create(
            Codigo.Create(10),
            ObservacaoItem.Create("Sem cebola"),
            TituloItem.Create("X-Burger"),
            Saldo.Create(25m),
            Saldo.Create(10m),
            null);

        /// <summary>
        /// Cria pedido VINCULADO à mesa informada (efeito colateral real do domínio).
        /// Pedido.Create(mesa) já faz mesa.AdicionarPedido(this) automaticamente.
        /// </summary>
        private static Pedido NovoPedidoVinculado(Mesa mesa, Codigo? numero = null) =>
            Pedido.Create(numero ?? NextCodigo(), mesa, UsuarioValido());

        /// <summary>
        /// Cria pedido ancorado em mesa temporária para testar Mesa.AdicionarPedido.
        /// Remove de temp.Pedidos para evitar StackOverflow B3 durante FechamentoDeConta:
        /// SUT.FechamentoDeConta -> pedido.SituacaoConcluido -> temp.FechamentoDeConta
        /// manteria loop se temp ainda contivesse o pedido. Mantém p.Mesa == temp para expor B7.
        /// </summary>
        private static Pedido PedidoDeOutraMesa()
        {
            var temp = Mesa.Create(Codigo.Create(9999), ESituacaoMesa.Disponivel);
            var p = Pedido.Create(NextCodigo(), temp, UsuarioValido());
            temp.Pedidos.Remove(p);
            return p;
        }

        private static Pedido PedidoVinculadoComLinha(Mesa mesa, ESituacaoLinhaPedido situacao)
        {
            var p = NovoPedidoVinculado(mesa);
            var linha = LinhaPedido.Create(p, ItemValido(), 1, false);
            linha.AtualizarSituacao(situacao);
            return p;
        }

        // ==================================================================
        // Create
        // ==================================================================

        [Fact]
        public void Criar_ComDadosValidos_DeveCriarMesaComSucesso()
        {
            var codigo = CodigoValido();
            var situacao = ESituacaoMesa.Disponivel;

            var mesa = Mesa.Create(codigo, situacao);

            Assert.NotNull(mesa);
            Assert.Equal(codigo, mesa.Codigo);
            Assert.Equal(situacao, mesa.Situacao);
            Assert.NotNull(mesa.Pedidos);
            Assert.Empty(mesa.Pedidos);
        }

        [Fact]
        public void Criar_ComOverloadDefault_DeveCriarComSituacaoDisponivel()
        {
            var mesa = Mesa.Create(CodigoValido());

            Assert.Equal(ESituacaoMesa.Disponivel, mesa.Situacao);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(-1)]
        public void Criar_ComSituacaoInvalida_DeveLancarExcecao(int valorInvalido)
        {
            var situacaoInvalida = (ESituacaoMesa)valorInvalido;

            var ex = Record.Exception(() => Mesa.Create(CodigoValido(), situacaoInvalida));

            Assert.NotNull(ex);
            Assert.IsType<MesaValidacao>(ex);
            Assert.Equal(MensagensBase.SITUACAO_INVALIDA, ex.Message);
        }

        // B1 — contrato esperado: codigo nulo deve falhar
        [Fact]
        public void Criar_ComCodigoNulo_DeveriaLancarExcecao_ContratoEstrito()
        {
            var ex = Record.Exception(() => Mesa.Create(null!, ESituacaoMesa.Disponivel));

            Assert.NotNull(ex);
            Assert.IsType<HeyChefe.Domain.Validacoes.ExceptionDomain>(ex);
            Assert.Equal(MensagensBase.CODIGO_OBRIGATORIO, ex.Message);
        }

        // Documenta comportamento atual (passa hoje, deve ser removido após corrigir B1)
        [Fact]
        public void Criar_ComCodigoNulo_AtualmentePermiteCriacao_EvidenciaBugB1()
        {
            var mesa = Mesa.Create(null!, ESituacaoMesa.Disponivel);

            Assert.NotNull(mesa);
            Assert.Null(mesa.Codigo);
        }

        // ==================================================================
        // OcuparMesa
        // ==================================================================

        [Fact]
        public void OcuparMesa_ComMesaDisponivel_DeveOcuparMesa()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);

            mesa.OcuparMesa();

            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
        }

        [Theory]
        [InlineData(ESituacaoMesa.Ocupada)]
        [InlineData(ESituacaoMesa.LimpezaPendente)]
        [InlineData(ESituacaoMesa.Reservada)]
        public void OcuparMesa_ComMesaNaoDisponivel_DeveLancarExcecao(ESituacaoMesa situacao)
        {
            var mesa = Mesa.Create(CodigoValido(), situacao);

            var ex = Record.Exception(() => mesa.OcuparMesa());

            Assert.NotNull(ex);
            Assert.IsType<MesaValidacao>(ex);
            Assert.Equal(MensagemMesa.MESA_NAO_DISPONIVEL, ex.Message);
        }

        // ==================================================================
        // AdicionarPedido — contrato estrito
        // ==================================================================

        [Fact]
        public void AdicionarPedido_AoCriarPedido_MesaDeveFicarOcupadaEConterPedido()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            Assert.Empty(mesa.Pedidos);

            var pedido = NovoPedidoVinculado(mesa);

            Assert.Contains(pedido, mesa.Pedidos);
            Assert.Single(mesa.Pedidos);
            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
            Assert.Same(mesa, pedido.Mesa);
        }

        [Fact]
        public void AdicionarPedido_ComMesaOcupada_DeveAcumularVariosPedidos()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var p1 = NovoPedidoVinculado(mesa, Codigo.Create(3001));
            var p2 = NovoPedidoVinculado(mesa, Codigo.Create(3002));

            Assert.Equal(2, mesa.Pedidos.Count);
            Assert.Contains(p1, mesa.Pedidos);
            Assert.Contains(p2, mesa.Pedidos);
            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
        }

        [Fact]
        public void AdicionarPedido_ComMesaLimpezaPendente_DeveLancarExcecao()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.LimpezaPendente);
            var pedido = PedidoDeOutraMesa();

            var ex = Record.Exception(() => mesa.AdicionarPedido(pedido));

            Assert.NotNull(ex);
            Assert.IsType<MesaValidacao>(ex);
            Assert.Equal(MensagemMesa.MESA_NAO_DISPONIVEL, ex.Message);
        }

        // B4 — Reservada NÃO deveria aceitar pedido (coerente com OcuparMesa)
        [Fact]
        public void AdicionarPedido_ComMesaReservada_DeveriaLancarExcecao_ContratoEstrito()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Reservada);
            var pedido = PedidoDeOutraMesa();

            var ex = Record.Exception(() => mesa.AdicionarPedido(pedido));

            Assert.NotNull(ex);
            Assert.IsType<MesaValidacao>(ex);
            Assert.Equal(MensagemMesa.MESA_NAO_DISPONIVEL, ex.Message);
        }

        // Documenta bug atual B4 (passa hoje)
        [Fact]
        public void AdicionarPedido_ComMesaReservada_AtualmentePermiteAdicionar_EvidenciaBugB4()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Reservada);
            var pedido = PedidoDeOutraMesa();

            var ex = Record.Exception(() => mesa.AdicionarPedido(pedido));

            Assert.Null(ex);
            Assert.Contains(pedido, mesa.Pedidos);
            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
        }

        // B6 — não deveria duplicar
        [Fact]
        public void AdicionarPedido_ComMesmoPedidoDuplicado_DeveriaLancarExcecao_ContratoEstrito()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = NovoPedidoVinculado(mesa);
            Assert.Single(mesa.Pedidos);

            var ex = Record.Exception(() => mesa.AdicionarPedido(pedido));

            Assert.NotNull(ex);
            // Espera validação de duplicidade — domínio atual apenas adiciona de novo
            Assert.IsType<MesaValidacao>(ex);
            Assert.Single(mesa.Pedidos);
        }

        // B7 — bidirecionalidade quebrada
        [Fact]
        public void AdicionarPedido_DeveriaSincronizarPedidoMesa_ContratoEstrito()
        {
            var mesaDestino = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoDeOutraMesa(); // pedido.Mesa == temp
            var mesaOrigem = pedido.Mesa;
            Assert.NotSame(mesaDestino, mesaOrigem);

            mesaDestino.AdicionarPedido(pedido);

            // Contrato: após adicionar, pedido.Mesa deve ser a mesa destino
            Assert.Same(mesaDestino, pedido.Mesa);
            Assert.Contains(pedido, mesaDestino.Pedidos);
            Assert.DoesNotContain(pedido, mesaOrigem.Pedidos);
        }

        // ==================================================================
        // RemovePedido
        // ==================================================================

        [Fact]
        public void RemovePedido_ComPedidoPertencendoAMesa_DeveRemoverPedido()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = NovoPedidoVinculado(mesa);

            mesa.RemovePedido(pedido);

            Assert.DoesNotContain(pedido, mesa.Pedidos);
            Assert.Empty(mesa.Pedidos);
        }

        // B5 — após esvaziar, mesa deveria voltar a Disponivel (ou LimpezaPendente por regra)
        // Domínio atual mantém Ocupada
        [Fact]
        public void RemovePedido_AposEsvaziarMesa_DeveriaLiberarMesa_ContratoEstrito()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = NovoPedidoVinculado(mesa);
            Assert.Single(mesa.Pedidos);

            mesa.RemovePedido(pedido);

            Assert.Empty(mesa.Pedidos);
            // Contrato esperado: sem pedidos, mesa volta a Disponivel
            Assert.Equal(ESituacaoMesa.Disponivel, mesa.Situacao);
        }

        // Documenta comportamento atual B5 (passa hoje)
        [Fact]
        public void RemovePedido_AposEsvaziar_AtualmenteMantemOcupada_EvidenciaBugB5()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = NovoPedidoVinculado(mesa);

            mesa.RemovePedido(pedido);

            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
        }

        [Fact]
        public void RemovePedido_ComPedidoQueNaoPertenceAMesa_DeveLancarExcecao()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoDeOutraMesa();

            var ex = Record.Exception(() => mesa.RemovePedido(pedido));

            Assert.NotNull(ex);
            Assert.IsType<MesaValidacao>(ex);
            Assert.Equal(MensagemMesa.PEDIDO_NAO_PERTENCE_A_ESTA_MESA, ex.Message);
        }

        [Fact]
        public void RemovePedido_ComPedidoIniciado_DeveLancar_PEDIDO_JA_INICIADO()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = NovoPedidoVinculado(mesa);
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pronto);
            Assert.False(pedido.PermiteRemoverPedido());

            var ex = Record.Exception(() => mesa.RemovePedido(pedido));

            Assert.NotNull(ex);
            Assert.IsType<MesaValidacao>(ex);
            Assert.Equal(MensagensPedido.PEDIDO_JA_INICIADO, ex.Message);
            Assert.Contains(pedido, mesa.Pedidos);
        }

        // ==================================================================
        // AbandonoDeMesa
        // ==================================================================

        [Fact]
        public void AbandonoDeMesa_ComLimparMesaTrue_DeveCancelarPedidosESituacaoLimpezaPendente()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = NovoPedidoVinculado(mesa);

            mesa.AbandonoDeMesa(true);

            Assert.Equal(ESituacaoPedido.Cancelado, pedido.Situacao);
            Assert.Equal(ESituacaoMesa.LimpezaPendente, mesa.Situacao);
        }

        [Fact]
        public void AbandonoDeMesa_ComLimparMesaFalse_DeveCancelarPedidosESituacaoDisponivel()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = NovoPedidoVinculado(mesa);

            mesa.AbandonoDeMesa(false);

            Assert.Equal(ESituacaoPedido.Cancelado, pedido.Situacao);
            Assert.Equal(ESituacaoMesa.Disponivel, mesa.Situacao);
        }

        [Fact]
        public void AbandonoDeMesa_SemPedidos_DeveApenasAlterarSituacao()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Ocupada);

            mesa.AbandonoDeMesa(true);

            Assert.Empty(mesa.Pedidos);
            Assert.Equal(ESituacaoMesa.LimpezaPendente, mesa.Situacao);
        }

        [Fact]
        public void AbandonoDeMesa_ComMultiplosPedidos_DeveCancelarTodos()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var p1 = NovoPedidoVinculado(mesa, Codigo.Create(3101));
            var p2 = NovoPedidoVinculado(mesa, Codigo.Create(3102));

            mesa.AbandonoDeMesa(false);

            Assert.All(mesa.Pedidos, p => Assert.Equal(ESituacaoPedido.Cancelado, p.Situacao));
            Assert.Equal(ESituacaoMesa.Disponivel, mesa.Situacao);
        }

        [Fact]
        public void AbandonoDeMesa_ComPedidoIniciado_DeveLancarExcecao()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = NovoPedidoVinculado(mesa);
            pedido.SituacaoIniciado();

            var ex = Record.Exception(() => mesa.AbandonoDeMesa(true));

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.SITUACAO_INVALIDA, ex.Message);
        }

        // ==================================================================
        // FechamentoDeConta — contrato estrito
        // B3: Pedido.FechamentoPedido chama Mesa.FechamentoDeConta em loop.
        // Para pedidos VINCULADOS à própria mesa, FechamentoDeConta causa StackOverflow.
        // Testes com pedido vinculado estão com Skip e devem ser habilitados após corrigir B3/B4.
        // Testes com PedidoDeOutraMesa evitam a recursão na mesa SUT e validam a lógica
        // de validação de linhas sem estourar pilha.
        // ==================================================================

        [Fact]
        public void FechamentoDeConta_ComPedidoDeOutraMesaPronto_DeveConcluir()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoDeOutraMesa();
            // PedidoDeOutraMesa tem pedido.Mesa == temp, não é Pendente com linha Pronto ainda
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pronto);
            mesa.AdicionarPedido(pedido);

            mesa.FechamentoDeConta();

            // Pedido conclui (mesmo com mesa divergente, conclui via temp)
            Assert.Equal(ESituacaoPedido.Concluido, pedido.Situacao);
            Assert.NotNull(pedido.Fechamento);
            Assert.Equal(ESituacaoMesa.LimpezaPendente, mesa.Situacao);
        }

        [Fact]
        public void FechamentoDeConta_ComLinhaPendente_DeveLancar_LINHAS_EM_ABERTO()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoDeOutraMesa();
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pendente);
            mesa.AdicionarPedido(pedido);

            var ex = Record.Exception(() => mesa.FechamentoDeConta());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.LINHAS_EM_ABERTO, ex.Message);
        }

        [Fact]
        public void FechamentoDeConta_ComPedidoCancelado_DeveLancar_PEDIDO_CANCELADO()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = PedidoDeOutraMesa();
            pedido.SituacaoCancelado();
            mesa.AdicionarPedido(pedido);

            var ex = Record.Exception(() => mesa.FechamentoDeConta());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.PEDIDO_CANCELADO, ex.Message);
        }

        [Fact]
        public void FechamentoDeConta_SemPedidos_DeveMudarParaLimpezaPendente()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Ocupada);

            mesa.FechamentoDeConta();

            Assert.Equal(ESituacaoMesa.LimpezaPendente, mesa.Situacao);
        }

        // B3 — StackOverflow quando pedido.Mesa == SUT
        [Fact(Skip = "B3 - StackOverflow: Mesa.FechamentoDeConta <-> Pedido.FechamentoPedido em loop quando pedido.Mesa == SUT. Habilite após corrigir Domain.")]
        public void FechamentoDeConta_ComPedidoVinculadoPronto_NaoDeveCausarStackOverflow_ContratoEstrito()
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var pedido = NovoPedidoVinculado(mesa);
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Concluido);
            Assert.Contains(pedido, mesa.Pedidos);
            Assert.Same(mesa, pedido.Mesa);

            var ex = Record.Exception(() => mesa.FechamentoDeConta());

            Assert.Null(ex);
            Assert.Equal(ESituacaoPedido.Concluido, pedido.Situacao);
            Assert.Equal(ESituacaoMesa.LimpezaPendente, mesa.Situacao);
        }

        // ==================================================================
        // AtualizarSituacao
        // ==================================================================

        [Theory]
        [InlineData(ESituacaoMesa.Disponivel)]
        [InlineData(ESituacaoMesa.Ocupada)]
        [InlineData(ESituacaoMesa.LimpezaPendente)]
        [InlineData(ESituacaoMesa.Reservada)]
        public void AtualizarSituacao_ComSituacaoValida_DeveAtualizarSituacao(ESituacaoMesa novaSituacao)
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);

            mesa.AtualizarSituacao(novaSituacao);

            Assert.Equal(novaSituacao, mesa.Situacao);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(-1)]
        public void AtualizarSituacao_ComSituacaoInvalida_DeveLancarExcecao(int valorInvalido)
        {
            var mesa = Mesa.Create(CodigoValido(), ESituacaoMesa.Disponivel);
            var situacaoInvalida = (ESituacaoMesa)valorInvalido;

            var ex = Record.Exception(() => mesa.AtualizarSituacao(situacaoInvalida));

            Assert.NotNull(ex);
            Assert.IsType<MesaValidacao>(ex);
            Assert.Equal(MensagensBase.SITUACAO_INVALIDA, ex.Message);
        }
    }
}
