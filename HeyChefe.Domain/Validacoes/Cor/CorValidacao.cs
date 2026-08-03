using Financ.Domain.Validacoes.Base;
using Financ.Domain.Validacoes.Movimentações;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes.Cor
{
    public class CorValidacao : BaseValidacao
    {
        public CorValidacao(string erro) : base(erro){}
        public static void Verifica(bool condicao, string mensagem)
        {
            VerificaExcessao<CorValidacao>(condicao, mensagem);
        }
    }
}
