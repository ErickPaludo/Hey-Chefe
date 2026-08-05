using HeyChefe.Domain.Validacoes.Base;
using HeyChefe.Domain.Validacoes.Cor;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Validacoes.Item
{
    public class ItemValidacao : BaseValidacao
    {
        public ItemValidacao(string erro) : base(erro) { }

        public static void Verifica(bool condicao, string mensagem)
        {
            VerificaExcessao<ItemValidacao>(condicao, mensagem);
        }
    }
}
