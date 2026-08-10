using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Entidades.Itens.Enums;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Pedidos;
using HeyChefe.Domain.Validacoes.Pedidos.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Entidades.Pedidos
{
    public class LinhaPedido : EntidadeBase
    {
        public Pedido Pedido { get; }
        public Item Item { get; }
        public ESituacaoLinhaPedido Situacao { get; private set; }
        public int Quantidade { get; private set; }
        public bool Cortesia { get; private set; }
        public LinhaPedido() { }

        private LinhaPedido(Pedido pedido, Item item, int quantidade,bool cortesia)
        {
            ValidaNulo.Verifica(pedido, MensagensPedido.PEDIDO_INVALIDO);
            ValidaNulo.Verifica(item, MensagensPedido.ITEM_INVALIDO);
            ValidaNulo.Verifica(quantidade, MensagensPedido.QUANTIDADE_NULA);
            ValidaNulo.Verifica(cortesia, MensagensPedido.CORTESIA_INVALIDA);

            ValidaQuantidade(quantidade);
            Pedido = pedido;
            Item = item;
            Quantidade = quantidade;
            Cortesia = cortesia;
            Situacao = ESituacaoLinhaPedido.Pendente;
        }
        private void ValidaQuantidade(int quantidade) =>
            PedidoValidacao.Verifica(quantidade <= 0, MensagensPedido.QUANTIDADE_INVALIDA);

        public static LinhaPedido Create(Pedido pedido, Item item, int quantidade,bool cortesia) =>
            new LinhaPedido(pedido, item, quantidade,cortesia);

        public bool Iniciado() => Situacao != ESituacaoLinhaPedido.Pendente && Situacao != ESituacaoLinhaPedido.Cancelado;
        public void AtualizarQuantidade(int quantidade)
        {
            ValidaQuantidade(quantidade);
            Quantidade = quantidade;
        }
        public void AtualizarSituacao(ESituacaoLinhaPedido situacao)
        {
            PedidoValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoLinhaPedido), situacao), MensagensBase.SITUACAO_INVALIDA);
            Situacao = situacao;
        }
        public void AtualizarCortesia(bool cortesia)
        {
            ValidaNulo.Verifica(cortesia, MensagensPedido.CORTESIA_INVALIDA);
            Cortesia = cortesia;
        }
    }
}
