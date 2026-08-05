using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Entidades.Itens.Enums;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Item;
using HeyChefe.Domain.Validacoes.Pedidos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Entidades.Pedidos
{
    public sealed class Pedido : EntidadeBase
    {
        public Codigo NumeroPedido { get; }
        public int Prioridade { get; private set; }
        public ESituacaoPedido Situacao { get; private set; }
        public Usuario Usuario { get; set; }
        private Pedido(Codigo numeroPedido, Usuario usuario, int prioridade)
        {
            Prioridade = prioridade;
            NumeroPedido = numeroPedido;
            Situacao = ESituacaoPedido.Pendente;
            Usuario = usuario;
        }

        public static Pedido Create(Codigo numeroPedido, Usuario usuario, int prioridade) =>
            new Pedido(numeroPedido, usuario, prioridade);

        public static Pedido Create(Codigo numeroPedido, Usuario usuario) => 
            new Pedido(numeroPedido, usuario, 0);

        public void AtualizarSituacao(ESituacaoPedido situacao)
        {
            PedidoValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoItem), situacao), MensagensBase.SITUACAO_INVALIDA);

            if (situacao == ESituacaoPedido.Cancelado)
                PedidoValidacao.Verifica(Situacao != ESituacaoPedido.Pendente, MensagensBase.SITUACAO_INVALIDA);

            Situacao = situacao;
        }
        public void AtualizarPrioridade(int prioridade)
        {
            Prioridade = prioridade;
        }
    }
}
