using Financ.Domain.Validacoes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes.Segurança
{
    public class AutenticacaoValidacao : BaseValidacao
    {
        public AutenticacaoValidacao(string erro) : base(erro) { }
        public static void Verifica(bool condicao, string mensagem)
        {
            VerificaExcessao<AutenticacaoValidacao>(condicao, mensagem);
        }
    }
}
