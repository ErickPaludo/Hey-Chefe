using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Validacoes.Pedidos.Mensagens
{
    public static class MensagensPedido
    {
        public static string  PEDIDO_INVALIDO => "O pedido é inválido.";

        public static string QUANTIDADE_INVALIDA => "A quantidade do pedido deve ser maior que zero.";

        public static string PRIORIDADE_OBRIGATORIA => "A prioridade do pedido é obrigatória.";

        public static string ITEM_INVALIDO => "O item do pedido é inválido.";

        public static string QUANTIDADE_NULA => "A quantidade do pedido não pode ser nula.";

        public static string LINHAS_EM_ABERTO => "O pedido possui itens não entregues.";

        public static string PEDIDO_INICIADO => "O pedido já foi iniciado";
        public static string PEDIDO_JA_INICIADO => "O pedido já foi iniciado.";
        public static string PEDIDO_CANCELADO => "O pedido está cancelado.";
        public static string SITUACAO_INVALIDA => "A situação do pedido é inválida.";

        public static string CORTESIA_INVALIDA => "A cortesia do pedido é inválida.";

        public static string PEDIDO_CONCLUIDO => "O pedido foi concluído.";
        public static string PEDIDO_PRONTO => "O pedido está pronto para entrega.";
    }
}
