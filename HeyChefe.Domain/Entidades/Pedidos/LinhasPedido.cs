using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Entidades.Itens.Enums;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Pedidos;
using HeyChefe.Domain.Validacoes.Pedidos.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Entidades.Pedidos
{
    public class LinhasPedido : EntidadeBase
    {
        public Pedido Pedido { get; }
        public Item Item { get; }
        public ESituacaoLinhaPedido Situacao { get; private set; }
        public int Quantidade { get; private set; }
        private LinhasPedido(Pedido pedido, Item item, int quantidade)
        {
            ValidaQuantidade(quantidade);
            Pedido = pedido;
            Item = item;
            Quantidade = quantidade;

            Situacao = ESituacaoLinhaPedido.Pendente;
        }
        private void ValidaQuantidade(int quantidade) =>
            PedidoValidacao.Verifica(quantidade <= 0, MensagensPedido.QUANTIDADE_INVALIDA);

        public static LinhasPedido Create(Pedido pedido, Item item, int quantidade) =>
            new LinhasPedido(pedido, item, quantidade);

        public void AtualizarQuantidade(int quantidade) =>
            Quantidade = quantidade;

        public void AtualizarSituacao(ESituacaoLinhaPedido situacao)
        {
            PedidoValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoLinhaPedido), situacao), MensagensBase.SITUACAO_INVALIDA);
            Situacao = situacao;
        }
    }
}
