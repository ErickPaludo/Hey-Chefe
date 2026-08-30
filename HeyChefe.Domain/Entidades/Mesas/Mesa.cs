using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Entidades.Mesas.Enums;
using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Codigo.Mensagens;
using HeyChefe.Domain.Validacoes.Mesas;
using HeyChefe.Domain.Validacoes.Mesas.Mensagens;
using HeyChefe.Domain.Validacoes.Pedidos.Mensagens;

namespace HeyChefe.Domain.Entidades.Mesas
{
    public sealed class Mesa : EntidadeBase
    {
        public Codigo Codigo { get; }
        public ESituacaoMesa Situacao { get; private set; }
        public Pedido? Pedido { get; private set; }

        public Mesa()
        {
        }

        private Mesa(Codigo codigo, ESituacaoMesa situacao)
        {
            ValidaNulo.Verifica(codigo, MensagensCodigo.CODIGO_OBRIGATORIO);
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
            ValidaNulo.Verifica(pedido, MensagemMesa.PEDIDO_DEVE_SER_INFORMADO);
            MesaValidacao.Verifica(pedido.Mesa != this, MensagemMesa.PEDIDO_NAO_PERTENCE_A_ESTA_MESA);
            MesaValidacao.Verifica(Situacao == ESituacaoMesa.LimpezaPendente, MensagemMesa.MESA_NAO_DISPONIVEL);
            MesaValidacao.Verifica(Pedido is not null, MensagemMesa.MESA_JA_POSSUI_PEDIDO);

            if (Situacao == ESituacaoMesa.Disponivel)
                OcuparMesa();

            Pedido = pedido;
            Situacao = ESituacaoMesa.Ocupada;
        }

        public void RemovePedido(Pedido pedido)
        {
            MesaValidacao.Verifica(Pedido is null, MensagemMesa.MESA_SEM_PEDIDO);
            MesaValidacao.Verifica(Pedido != pedido, MensagemMesa.PEDIDO_NAO_PERTENCE_A_ESTA_MESA);
            MesaValidacao.Verifica(!pedido.PermiteRemoverPedido(), MensagensPedido.PEDIDO_JA_INICIADO);
        }

        public void AbandonoDeMesa(bool limparMesa)
        {
            if (Pedido is not null)
                MesaValidacao.Verifica(Pedido.PermiteRemoverPedido(), MensagemMesa.EXISTEM_PEDIDOS_CONCLUIDOS);
            
            Situacao = limparMesa ? ESituacaoMesa.LimpezaPendente : ESituacaoMesa.Disponivel;
        }

        public void FechamentoDeConta()
        {
            ValidaNulo.Verifica(Pedido,MensagemMesa.MESA_SEM_PEDIDO);
            Pedido.SituacaoConcluido();
            Situacao = ESituacaoMesa.LimpezaPendente;
        }

        public void AtualizarSituacao(ESituacaoMesa situacao)
        {
            MesaValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoMesa), situacao), MensagensBase.SITUACAO_INVALIDA);
            Situacao = situacao;
        }
    }
}