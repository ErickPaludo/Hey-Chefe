using HeyChefe.Domain.Validacoes.Base;
using HeyChefe.Domain.Validacoes.Mesas;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Validacoes.Pedidos
{
    public class PedidoValidacao : BaseValidacao
    {
        public PedidoValidacao(string erro) : base(erro){}
        public static void Verifica(bool condicao, string mensagem)
        {
            VerificaExcessao<PedidoValidacao>(condicao, mensagem);
        }
    }
}
