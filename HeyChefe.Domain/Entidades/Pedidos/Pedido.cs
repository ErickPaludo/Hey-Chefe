using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Entidades.Itens.Enums;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Item;
using HeyChefe.Domain.Validacoes.Pedidos;
using HeyChefe.Domain.Validacoes.Pedidos.Mensagens;
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
        public Usuario Usuario { get; }
        public DateTime? Fechamento { get; private set; }
        public List<LinhasPedido> LinhasPedido { get; private set; } = new List<LinhasPedido>();
        private Pedido(Codigo numeroPedido, Usuario usuario, int prioridade)
        {
            ValidaNulo.Verifica(prioridade, MensagensPedido.PRIORIDADE_OBRIGATORIA);
            ValidaNulo.Verifica(numeroPedido, MensagensBase.CODIGO_OBRIGATORIO);
            ValidaNulo.Verifica(usuario, MensagensBase.USUARIO_OBRIGATORIO);

            Prioridade = prioridade;
            NumeroPedido = numeroPedido;
            Situacao = ESituacaoPedido.Pendente;
            Usuario = usuario;
        }

        public static Pedido Create(Codigo numeroPedido, Usuario usuario, int prioridade) =>
            new Pedido(numeroPedido, usuario, prioridade);

        public static Pedido Create(Codigo numeroPedido, Usuario usuario) =>
            new Pedido(numeroPedido, usuario, 0);

        public bool PermiteRemoverPedido() => !LinhasPedido.Any(p => p.Iniciado());
        #region Atualização
        #region Situacao
        public void SituacaoPendente()
        {
            PedidoValidacao.Verifica(LinhasPedido.Any(p => p.Iniciado()), MensagensPedido.PEDIDO_INICIADO);
            Situacao = ESituacaoPedido.Pendente;
            Fechamento = null;
        }
        public void SituacaoIniciado()
        {
            PedidoValidacao.Verifica(Situacao != ESituacaoPedido.Pendente, MensagensPedido.PEDIDO_JA_INICIADO);
            Situacao = ESituacaoPedido.Iniciado;
        }
        public void SituacaoConcluido()
        {
            PedidoValidacao.Verifica(Situacao == ESituacaoPedido.Cancelado, MensagensPedido.PEDIDO_CANCELADO);
            FechamentoPedido();
        }
        public void SituacaoCancelado()
        {
            PedidoValidacao.Verifica(Situacao != ESituacaoPedido.Pendente, MensagensPedido.SITUACAO_INVALIDA);
            Situacao = ESituacaoPedido.Cancelado;
            Fechamento = null;
        } 
        private void FechamentoPedido()
        {
            PedidoValidacao.Verifica(LinhasPedido.Any(p => !p.Iniciado()), MensagensPedido.LINHAS_EM_ABERTO);
            Situacao = ESituacaoPedido.Concluido;
            Fechamento = DateTime.UtcNow;
        }
        #endregion
        #region Prioridade
        public void AtualizarPrioridade(int prioridade)
        {
            ValidaNulo.Verifica(prioridade, MensagensPedido.PRIORIDADE_OBRIGATORIA);
            Prioridade = prioridade;
        }
        #endregion
        #endregion
    }
}
