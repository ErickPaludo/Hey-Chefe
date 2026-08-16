using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Entidades.Mesas.Enums;
using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Mesas;
using HeyChefe.Domain.Validacoes.Mesas.Mensagens;
using HeyChefe.Domain.Validacoes.Pedidos.Mensagens;

namespace HeyChefe.Domain.Entidades.Mesas
{
    public sealed class Mesa : EntidadeBase
    {
        public Codigo Codigo { get; }
        public ESituacaoMesa Situacao { get; private set; }
        public List<Pedido> Pedidos { get; private set; } = new List<Pedido>();
        public Mesa() { }
        private Mesa(Codigo codigo, ESituacaoMesa situacao)
        {
            MesaValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoMesa), situacao), MensagensBase.SITUACAO_INVALIDA);
            Codigo = codigo;
            Situacao = situacao;
        }

        public static Mesa Create(Codigo codigo, ESituacaoMesa situacao) =>
            new Mesa(codigo, situacao);

        public static Mesa Create(Codigo codigo) =>
        new Mesa(codigo, ESituacaoMesa.Disponivel);
        public void OcuparMesa()
        {
            MesaValidacao.Verifica(Situacao != ESituacaoMesa.Disponivel, MensagemMesa.MESA_NAO_DISPONIVEL);
            Situacao = ESituacaoMesa.Ocupada;
        }
        public void AdicionarPedido(Pedido pedido)
        {
            MesaValidacao.Verifica(Situacao == ESituacaoMesa.LimpezaPendente, MensagemMesa.MESA_NAO_DISPONIVEL);

            if (Situacao == ESituacaoMesa.Disponivel)
                OcuparMesa();

            Pedidos.Add(pedido);
            Situacao = ESituacaoMesa.Ocupada;
        }
        public void RemovePedido(Pedido pedido)
        {
            MesaValidacao.Verifica(!Pedidos.Contains(pedido), MensagemMesa.PEDIDO_NAO_PERTENCE_A_ESTA_MESA);
            MesaValidacao.Verifica(!pedido.PermiteRemoverPedido(), MensagensPedido.PEDIDO_JA_INICIADO);

            Pedidos.Remove(pedido);
            Situacao = ESituacaoMesa.Ocupada;
        }
        public void AbandonoDeMesa(bool limparMesa)
        {
            AlteraSituacaoPedidos(ESituacaoPedido.Cancelado);
            Situacao = limparMesa ? ESituacaoMesa.LimpezaPendente : ESituacaoMesa.Disponivel;
        }
        public void FechamentoDeConta()
        {
            AlteraSituacaoPedidos(ESituacaoPedido.Concluido);
            Situacao = ESituacaoMesa.LimpezaPendente;
        }
        public void AtualizarSituacao(ESituacaoMesa situacao)
        {
            MesaValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoMesa), situacao), MensagensBase.SITUACAO_INVALIDA);
            Situacao = situacao;
        }

        private void AlteraSituacaoPedidos(ESituacaoPedido situacao)
        {
            foreach (var pedido in Pedidos)
            {
                if (situacao == ESituacaoPedido.Cancelado)
                    pedido.SituacaoCancelado();

                else if (situacao == ESituacaoPedido.Concluido)
                    pedido.SituacaoConcluido();
            }
        }


    }
}
