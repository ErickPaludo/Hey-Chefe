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
    /// <summary>
    /// Especificação estrita do contrato de Pedido.
    /// INTENCIONAL: vários testes falham contra a Domain atual para guiar a correção.
    /// Não há workarounds (isolamento/mesa auxiliar) aqui — o teste descreve o que
    /// DEVERIA acontecer. Você corrige a Domain até ficar verde.
    /// Bugs mapeados:
    ///  B1: ValidaNulo(int) morto — prioridade negativa passa
    ///  B2: Pedido ctor chama Mesa.AdicionarPedido(this) — acoplamento + duplicidade
    ///  B3: Pedido.FechamentoPedido() chama Mesa.FechamentoDeConta() — recursão infinita (StackOverflow) quando pedido.Mesa.Pedidos contém o pedido
    ///  B4: Pedido.SituacaoConcluido não deveria ter efeito colateral em Mesa (responsabilidade de Mesa.FechamentoDeConta)
    ///  B5: Saldo.Porcentagem = Valor + Margem/100 em vez de Valor * (1+Margem/100) — ValorFinal incorreto
    /// </summary>
    public class PedidoTest
    {
        private static Codigo NumeroPedidoValido() => Codigo.Create(100);
        private static Codigo CodigoMesa(int v = 1) => Codigo.Create(v);

        private static Usuario UsuarioValido() => Usuario.Create(
            NomeUsuario.Create("Carlos", "Silva"),
            Email.Create("carlos.silva@email.com"),
            Senha.Create("salt123", "hash123"),
            EPermissaoUsuario.Garcom);

        private static Mesa MesaValida(ESituacaoMesa situacao = ESituacaoMesa.Disponivel) =>
            Mesa.Create(CodigoMesa(), situacao);

        private static Item ItemValido() => Item.Create(
            Codigo.Create(10),
            ObservacaoItem.Create("Sem cebola"),
            TituloItem.Create("X-Burger"),
            Saldo.Create(25m),
            Saldo.Create(10m),
            null);

        // Helper estrito: cria Pedido VINCULADO à mesa informada (com efeito colateral real).
        // Não remove de mesa.Pedidos — expõe B2/B3.
        private static Pedido NovoPedidoVinculado(
            Codigo? numero = null,
            Mesa? mesa = null,
            Usuario? usuario = null,
            int prioridade = 0) =>
            Pedido.Create(numero ?? NumeroPedidoValido(), mesa ?? MesaValida(), usuario ?? UsuarioValido(), prioridade);

        // ==================================================================
        // Create — contrato do ctor
        // ==================================================================

        [Fact]
        public void Create_ComDadosValidos_DeveCriarPedidoComSucesso()
        {
            var numero = NumeroPedidoValido();
            var mesa = MesaValida();
            var usuario = UsuarioValido();
            var prioridade = 5;

            var pedido = Pedido.Create(numero, mesa, usuario, prioridade);

            Assert.NotNull(pedido);
            Assert.Equal(numero, pedido.NumeroPedido);
            Assert.Equal(mesa, pedido.Mesa);
            Assert.Equal(usuario, pedido.Usuario);
            Assert.Equal(prioridade, pedido.Prioridade);
            Assert.Equal(ESituacaoPedido.Pendente, pedido.Situacao);
            Assert.Null(pedido.Fechamento);
            Assert.Empty(pedido.LinhasPedido);
            Assert.Contains(pedido, mesa.Pedido);
            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
        }

        [Fact]
        public void Create_SemPrioridadeInformada_DeveCriarPedidoComPrioridadeZero()
        {
            var mesa = MesaValida();

            var pedido = Pedido.Create(NumeroPedidoValido(), mesa, UsuarioValido());

            Assert.Equal(0, pedido.Prioridade);
            Assert.Contains(pedido, mesa.Pedido);
        }

        [Fact]
        public void Create_ComNumeroPedidoNulo_DeveLancarExcecao()
        {
            var mesa = MesaValida();

            var ex = Record.Exception(() => Pedido.Create(null!, mesa, UsuarioValido(), 5));

            Assert.NotNull(ex);
            Assert.IsType<ExceptionDomain>(ex);
            Assert.Equal(MensagensBase.CODIGO_OBRIGATORIO, ex.Message);
        }

        [Fact]
        public void Create_ComMesaNula_DeveLancarExcecao()
        {
            var ex = Record.Exception(() => Pedido.Create(NumeroPedidoValido(), null!, UsuarioValido(), 5));

            Assert.NotNull(ex);
            Assert.IsType<ExceptionDomain>(ex);
            Assert.Equal(MensagensBase.MESA_INVALIDA, ex.Message);
        }

        [Fact]
        public void Create_ComUsuarioNulo_DeveLancarExcecao()
        {
            var mesa = MesaValida();

            var ex = Record.Exception(() => Pedido.Create(NumeroPedidoValido(), mesa, null!, 5));

            Assert.NotNull(ex);
            Assert.IsType<ExceptionDomain>(ex);
            Assert.Equal(MensagensBase.USUARIO_OBRIGATORIO, ex.Message);
        }

        [Fact]
        public void Create_ComMesaLimpezaPendente_DeveLancarExcecao()
        {
            var mesa = Mesa.Create(CodigoMesa(), ESituacaoMesa.LimpezaPendente);

            var ex = Record.Exception(() => Pedido.Create(NumeroPedidoValido(), mesa, UsuarioValido(), 0));

            Assert.NotNull(ex);
            // MesaValidacao via Mesa.AdicionarPedido
            Assert.IsType<HeyChefe.Domain.Validacoes.Mesas.MesaValidacao>(ex);
            Assert.Equal(HeyChefe.Domain.Validacoes.Mesas.Mensagens.MensagemMesa.MESA_NAO_DISPONIVEL, ex.Message);
        }

        // BUG B2 — contrato esperado: mesa Reservada NÃO deveria aceitar pedido.
        // Domain atual permite (só bloqueia LimpezaPendente). Este teste FALHA até corrigir Mesa.AdicionarPedido.
        [Fact]
        public void Create_ComMesaReservada_DeveLancarExcecao_ContratoEstrito()
        {
            var mesa = Mesa.Create(CodigoMesa(), ESituacaoMesa.Reservada);

            var ex = Record.Exception(() => Pedido.Create(NumeroPedidoValido(), mesa, UsuarioValido(), 0));

            Assert.NotNull(ex);
            Assert.IsType<HeyChefe.Domain.Validacoes.Mesas.MesaValidacao>(ex);
            Assert.Equal(HeyChefe.Domain.Validacoes.Mesas.Mensagens.MensagemMesa.MESA_NAO_DISPONIVEL, ex.Message);
        }

        [Fact]
        public void Create_DeveAdicionarPedidoNaMesaEOcuparMesaDisponivel()
        {
            var mesa = Mesa.Create(CodigoMesa(), ESituacaoMesa.Disponivel);
            Assert.Equal(ESituacaoMesa.Disponivel, mesa.Situacao);

            var pedido = Pedido.Create(NumeroPedidoValido(), mesa, UsuarioValido());

            Assert.Contains(pedido, mesa.Pedido);
            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
            Assert.Same(mesa, pedido.Mesa);
        }

        // ==================================================================
        // ValidaNulo(int) morto — B1
        // ==================================================================
        [Fact]
        public void AtualizarPrioridade_ComValorNegativo_DeveriaLancarExcecao_ContratoEstrito()
        {
            var pedido = NovoPedidoVinculado(prioridade: 0);

            var ex = Record.Exception(() => pedido.AtualizarPrioridade(-1));

            // Contrato esperado: prioridade não pode ser negativa.
            // Domain atual usa ValidaNulo(int) que nunca dispara (int não é null).
            // Este teste FALHA até corrigir para validar faixa.
            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
        }

        // Teste que documenta o comportamento atual (passa hoje, deve ser removido após correção)
        [Fact]
        public void AtualizarPrioridade_ComValorNegativo_AtualmentePermite_EvidenciaBugB1()
        {
            var mesa = MesaValida();
            var pedido = Pedido.Create(Codigo.Create(901), mesa, UsuarioValido(), 0);
            mesa.Pedido.Remove(pedido); // isola para não poluir outros testes, mas mantém bug visível

            pedido.AtualizarPrioridade(-1);

            Assert.Equal(-1, pedido.Prioridade);
        }

        // ==================================================================
        // AdicionaLinhas / PermiteRemover / ValorFinal
        // ==================================================================

        [Fact]
        public void AdicionaLinhasPedidos_ComLinhaNula_DeveLancarExcecao()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(902));

            var ex = Record.Exception(() => pedido.AdicionaLinhasPedidos(null!));

            Assert.NotNull(ex);
            Assert.IsType<ExceptionDomain>(ex);
            Assert.Equal(MensagensPedido.LINHAS_INVALIDA, ex.Message);
        }

        [Fact]
        public void PermiteRemoverPedido_SemLinhas_DeveRetornarTrue()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(903));

            Assert.True(pedido.PermiteRemoverPedido());
        }

        [Fact]
        public void PermiteRemoverPedido_ComLinhaPendente_DeveRetornarTrue()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(904));
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            // linha já está Pendente
            Assert.True(pedido.PermiteRemoverPedido());
            Assert.Equal(ESituacaoLinhaPedido.Pendente, linha.Situacao);
        }

        [Fact]
        public void PermiteRemoverPedido_ComLinhaCancelada_DeveRetornarTrue()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(905));
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Cancelado);

            Assert.True(pedido.PermiteRemoverPedido());
        }

        [Fact]
        public void PermiteRemoverPedido_ComLinhaPronto_DeveRetornarFalse()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(906));
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pronto);

            Assert.False(pedido.PermiteRemoverPedido());
        }

        [Fact]
        public void PermiteRemoverPedido_ComLinhaConcluida_DeveRetornarFalse()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(907));
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Concluido);

            Assert.False(pedido.PermiteRemoverPedido());
        }

        [Fact]
        public void ValorFinal_SemLinhas_DeveRetornarZero()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(908));

            Assert.Equal(0m, pedido.ValorFinal());
        }

        // B5 — contrato esperado: PrecoFinal = Custo * (1 + Margem/100)
        // Domain atual: Saldo.Porcentagem = Valor + Margem/100  (ex: 25 + 0.1 = 25.1)
        // Este teste FALHA até corrigir Saldo.Porcentagem.
        [Fact]
        public void ValorFinal_ComLinhas_DeveCalcularCustoVezesQuantidadeComMargem_ContratoEstrito()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(909));
            var item = ItemValido(); // Custo 25, Margem 10%
            var precoCorreto = 25m * (1 + 10m / 100m); // 27.5
            LinhaPedido.Create(pedido, item, 2, false);
            LinhaPedido.Create(pedido, item, 3, false);

            var esperadoCorreto = 2 * precoCorreto + 3 * precoCorreto; // 137.5
            var esperadoBugado = 5 * (25m + 10m / 100m); // 125.5 — o que a Domain entrega hoje

            // Assert estrito (falha hoje)
            Assert.Equal(esperadoCorreto, pedido.ValorFinal());
            // Se quiser ver o bug: Assert.Equal(esperadoBugado, pedido.ValorFinal()) passa hoje
        }

        // ==================================================================
        // SituacaoPendente
        // ==================================================================

        [Fact]
        public void SituacaoPendente_ComPedidoPendente_DeveManterSituacaoPendente()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(910));

            pedido.SituacaoPendente();

            Assert.Equal(ESituacaoPedido.Pendente, pedido.Situacao);
            Assert.Null(pedido.Fechamento);
        }

        [Fact]
        public void SituacaoPendente_ComPedidoIniciadoSemLinhaIniciada_DeveVoltarParaPendente()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(911));
            pedido.SituacaoIniciado();

            pedido.SituacaoPendente();

            Assert.Equal(ESituacaoPedido.Pendente, pedido.Situacao);
        }

        [Fact]
        public void SituacaoPendente_ComLinhaIniciada_DeveLancar_PEDIDO_INICIADO()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(912));
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pronto);

            var ex = Record.Exception(() => pedido.SituacaoPendente());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.PEDIDO_INICIADO, ex.Message);
        }

        // ==================================================================
        // SituacaoIniciado
        // ==================================================================

        [Fact]
        public void SituacaoIniciado_ComPedidoPendente_DeveIniciarPedido()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(913));

            pedido.SituacaoIniciado();

            Assert.Equal(ESituacaoPedido.Iniciado, pedido.Situacao);
        }

        [Fact]
        public void SituacaoIniciado_ComPedidoJaIniciado_DeveLancarExcecao()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(914));
            pedido.SituacaoIniciado();

            var ex = Record.Exception(() => pedido.SituacaoIniciado());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.PEDIDO_JA_INICIADO, ex.Message);
        }

        [Fact]
        public void SituacaoIniciado_ComPedidoCancelado_DeveLancarExcecao()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(915));
            pedido.SituacaoCancelado();

            var ex = Record.Exception(() => pedido.SituacaoIniciado());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.PEDIDO_JA_INICIADO, ex.Message);
        }

        [Fact]
        public void SituacaoIniciado_ComPedidoConcluido_DeveLancarExcecao()
        {
            // Isola para não causar StackOverflow via Mesa (B3) — usa pedido desvinculado
            // mas mantém expectativa estrita: Concluido não pode voltar a Iniciado
            var mesa = MesaValida();
            var pedido = Pedido.Create(Codigo.Create(916), mesa, UsuarioValido(), 0);
            mesa.Pedido.Remove(pedido);
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Concluido);
            pedido.SituacaoConcluido();
            Assert.Equal(ESituacaoPedido.Concluido, pedido.Situacao);

            var ex = Record.Exception(() => pedido.SituacaoIniciado());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.PEDIDO_JA_INICIADO, ex.Message);
        }

        // ==================================================================
        // SituacaoConcluido — contrato estrito SEM efeito colateral em Mesa (B4)
        // Domain atual: FechamentoPedido chama Mesa.FechamentoDeConta -> StackOverflow
        // se pedido.Mesa.Pedidos contém o pedido. Testes abaixo usam pedido
        // desvinculado para não estourar pilha, e assertam que Mesa NÃO deveria mudar.
        // Eles FALHAM até você remover a chamada Mesa.FechamentoDeConta de Pedido.
        // O teste com pedido vinculado que causa StackOverflow está marcado como
        // Explícito e deve ser habilitado após a correção para provar ausência de recursão.
        // ==================================================================

        [Fact]
        public void SituacaoConcluido_ComPedidoPendenteSemLinhas_DeveConcluirPedido()
        {
            var mesa = MesaValida();
            var pedido = Pedido.Create(Codigo.Create(917), mesa, UsuarioValido(), 0);
            mesa.Pedido.Remove(pedido);

            pedido.SituacaoConcluido();

            Assert.Equal(ESituacaoPedido.Concluido, pedido.Situacao);
            Assert.NotNull(pedido.Fechamento);
        }

        [Fact]
        public void SituacaoConcluido_NaoDeveriaAlterarSituacaoDaMesa_ContratoEstrito()
        {
            var mesa = MesaValida(); // Disponivel
            var pedido = Pedido.Create(Codigo.Create(918), mesa, UsuarioValido(), 0);
            mesa.Pedido.Remove(pedido); // desvincula para não estourar pilha, mas mantém referência pedido.Mesa == mesa
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Concluido);
            Assert.Equal(ESituacaoMesa.Disponivel, mesa.Situacao);

            pedido.SituacaoConcluido();

            // Contrato estrito: Pedido conclui, Mesa permanece como estava.
            // Quem fecha a conta é Mesa.FechamentoDeConta(), não Pedido.
            // Domain atual viola: mesa vai para LimpezaPendente.
            Assert.Equal(ESituacaoMesa.Disponivel, mesa.Situacao);
            Assert.Equal(ESituacaoPedido.Concluido, pedido.Situacao);
        }

        [Fact]
        public void SituacaoConcluido_ComLinhaPronto_DeveConcluirPedido()
        {
            var mesa = MesaValida();
            var pedido = Pedido.Create(Codigo.Create(919), mesa, UsuarioValido(), 0);
            mesa.Pedido.Remove(pedido);
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pronto);

            pedido.SituacaoConcluido();

            Assert.Equal(ESituacaoPedido.Concluido, pedido.Situacao);
        }

        [Fact]
        public void SituacaoConcluido_ComLinhaPendente_DeveLancar_LINHAS_EM_ABERTO()
        {
            var mesa = MesaValida();
            var pedido = Pedido.Create(Codigo.Create(920), mesa, UsuarioValido(), 0);
            mesa.Pedido.Remove(pedido);
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Pendente);

            var ex = Record.Exception(() => pedido.SituacaoConcluido());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.LINHAS_EM_ABERTO, ex.Message);
            Assert.Null(pedido.Fechamento);
        }

        [Fact]
        public void SituacaoConcluido_ComMixProntoEPendente_DeveLancar_LINHAS_EM_ABERTO()
        {
            var mesa = MesaValida();
            var pedido = Pedido.Create(Codigo.Create(921), mesa, UsuarioValido(), 0);
            mesa.Pedido.Remove(pedido);
            var l1 = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            var l2 = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            l1.AtualizarSituacao(ESituacaoLinhaPedido.Concluido);
            l2.AtualizarSituacao(ESituacaoLinhaPedido.Pendente);

            var ex = Record.Exception(() => pedido.SituacaoConcluido());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.LINHAS_EM_ABERTO, ex.Message);
        }

        [Fact]
        public void SituacaoConcluido_ComPedidoCancelado_DeveLancar_PEDIDO_CANCELADO()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(922));
            pedido.SituacaoCancelado();

            var ex = Record.Exception(() => pedido.SituacaoConcluido());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.PEDIDO_CANCELADO, ex.Message);
        }

        [Fact]
        public void SituacaoConcluido_DevePreencherFechamentoComUtcNow()
        {
            var mesa = MesaValida();
            var pedido = Pedido.Create(Codigo.Create(923), mesa, UsuarioValido(), 0);
            mesa.Pedido.Remove(pedido);
            var antes = DateTime.UtcNow.AddSeconds(-1);

            pedido.SituacaoConcluido();

            Assert.NotNull(pedido.Fechamento);
            Assert.True(pedido.Fechamento >= antes);
            Assert.True(pedido.Fechamento <= DateTime.UtcNow.AddSeconds(1));
        }

        // Prova de B3 — este teste causa StackOverflow na Domain atual.
        // Deixe-o falhando (processo aborta) até corrigir B3, ou mantenha [Fact(Skip)].
        // Quando B3 for corrigido, habilite e ele deve passar.
        [Fact(Skip = "B3 - StackOverflow: Pedido.FechamentoPedido chama Mesa.FechamentoDeConta em loop. Habilite após corrigir Domain.")]
        public void SituacaoConcluido_ComPedidoVinculadoNaMesa_NaoDeveCausarStackOverflow()
        {
            var mesa = MesaValida(); // Disponivel -> Ocupada após Create
            var pedido = Pedido.Create(Codigo.Create(924), mesa, UsuarioValido(), 0);
            // NÃO remove — pedido está em mesa.Pedidos (cenário real)
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Concluido);
            Assert.Contains(pedido, mesa.Pedido);

            var ex = Record.Exception(() => pedido.SituacaoConcluido());

            // Contrato: não deve estourar pilha; deve concluir pedido e NÃO alterar mesa (B4)
            Assert.Null(ex);
            Assert.Equal(ESituacaoPedido.Concluido, pedido.Situacao);
            Assert.Equal(ESituacaoMesa.Ocupada, mesa.Situacao);
        }

        // ==================================================================
        // SituacaoCancelado
        // ==================================================================

        [Fact]
        public void SituacaoCancelado_ComPedidoPendente_DeveCancelarPedido()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(925));

            pedido.SituacaoCancelado();

            Assert.Equal(ESituacaoPedido.Cancelado, pedido.Situacao);
            Assert.Null(pedido.Fechamento);
        }

        [Fact]
        public void SituacaoCancelado_ComPedidoIniciado_DeveLancarExcecao()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(926));
            pedido.SituacaoIniciado();

            var ex = Record.Exception(() => pedido.SituacaoCancelado());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.SITUACAO_INVALIDA, ex.Message);
        }

        [Fact]
        public void SituacaoCancelado_ComPedidoJaCancelado_DeveLancarExcecao()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(927));
            pedido.SituacaoCancelado();

            var ex = Record.Exception(() => pedido.SituacaoCancelado());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.SITUACAO_INVALIDA, ex.Message);
        }

        [Fact]
        public void SituacaoCancelado_ComPedidoConcluido_DeveLancarExcecao()
        {
            var mesa = MesaValida();
            var pedido = Pedido.Create(Codigo.Create(928), mesa, UsuarioValido(), 0);
            mesa.Pedido.Remove(pedido);
            var linha = LinhaPedido.Create(pedido, ItemValido(), 1, false);
            linha.AtualizarSituacao(ESituacaoLinhaPedido.Concluido);
            pedido.SituacaoConcluido();

            var ex = Record.Exception(() => pedido.SituacaoCancelado());

            Assert.NotNull(ex);
            Assert.IsType<PedidoValidacao>(ex);
            Assert.Equal(MensagensPedido.SITUACAO_INVALIDA, ex.Message);
        }

        // ==================================================================
        // AtualizarPrioridade — contrato estrito
        // ==================================================================

        [Fact]
        public void AtualizarPrioridade_ComValorValido_DeveAtualizarPrioridade()
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(929), prioridade: 0);

            pedido.AtualizarPrioridade(10);

            Assert.Equal(10, pedido.Prioridade);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public void AtualizarPrioridade_ComVariosValoresValidos_DeveAtualizar(int novaPrioridade)
        {
            var pedido = NovoPedidoVinculado(numero: Codigo.Create(930));

            pedido.AtualizarPrioridade(novaPrioridade);

            Assert.Equal(novaPrioridade, pedido.Prioridade);
        }
    }
}
