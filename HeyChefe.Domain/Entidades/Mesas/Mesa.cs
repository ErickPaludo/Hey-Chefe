using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Entidades.Mesas.Enums;
using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Base;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Mesas;
using HeyChefe.Domain.Validacoes.Mesas.Mensagens;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Entidades.Mesas
{
    public sealed class Mesa : EntidadeBase
    {
        public Codigo Codigo { get; }
        public ESituacaoMesa Situacao { get; private set; }
        public List<Pedido> Pedidos { get; private set; } = new List<Pedido>();
        private Mesa(Codigo codigo, ESituacaoMesa situacao)
        {
            MesaValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoMesa), situacao), MensagensBase.SITUACAO_INVALIDA);
            Codigo = codigo;
            Situacao = situacao;
        }

        public static Mesa Criar(Codigo codigo, ESituacaoMesa situacao) =>
            new Mesa(codigo, situacao);

        public void AdicionarPedido(Pedido pedido)
        {
            MesaValidacao.Verifica(Situacao == ESituacaoMesa.LimpezaPendente, MensagemMesa.MESA_NAO_DISPONIVEL);
            Pedidos.Add(pedido);
            Situacao = ESituacaoMesa.Ocupada;
        }

        public void FechamentoDeConta()
        {
            AlteraSituacaoPedidos(ESituacaoPedido.Concluido);
            Situacao = ESituacaoMesa.LimpezaPendente;
        }

        public void AbandonoDeMesa(bool limparMesa)
        {
            AlteraSituacaoPedidos(ESituacaoPedido.Cancelado);
            Situacao = limparMesa ? ESituacaoMesa.LimpezaPendente : ESituacaoMesa.Disponivel;
        }

        private void AlteraSituacaoPedidos(ESituacaoPedido situacao)
        {
            foreach (var pedido in Pedidos)
            {
                pedido.AtualizarSituacao(situacao);
            }
        }

        public void AtualizarSituacao(ESituacaoMesa situacao)
        {
            MesaValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoMesa), situacao), MensagensBase.SITUACAO_INVALIDA);
            Situacao = situacao;
        }

    }
}
