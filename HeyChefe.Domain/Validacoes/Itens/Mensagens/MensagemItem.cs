using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Validacoes.Item.Mensagens
{
    public static class MensagemItem
    {
        public static string VALOR_NULO => "O valor não pode ser nulo.";
        public static string VALOR_INVALIDO => "O valor deve ser maior que 0.";
        public static string MARGEM_LUCRO_INVALIDA => "A margem de lucro não pode ser negativa.";
    }
}
