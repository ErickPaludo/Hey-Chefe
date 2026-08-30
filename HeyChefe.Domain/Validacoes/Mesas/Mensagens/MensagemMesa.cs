using HeyChefe.Domain.Entidades;
using HeyChefe.Domain.Validacoes.Base;
using HeyChefe.Domain.Validacoes.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Validacoes.Mesas.Mensagens
{
    public static class MensagemMesa
    {
        public static string MESA_NAO_DISPONIVEL = "A mesa não está disponível para adicionar pedidos.";

        public static string PEDIDO_NAO_PERTENCE_A_ESTA_MESA => "O pedido não pertence a esta mesa.";

        public static string MESA_INDISPONIVEL => "Mesa indisponível no momento.";
        public static string MESA_JA_POSSUI_PEDIDO => "Mesa já possui pedido.";
        public static string PEDIDO_DEVE_SER_INFORMADO => "Pedido não informado.";
        public static string EXISTEM_PEDIDOS_CONCLUIDOS => "Existem pedidos já iniciados/concluidos para esta mesa.";
        public static string MESA_SEM_PEDIDO => "Esta mesa não possui pedidos.";
    }
}
