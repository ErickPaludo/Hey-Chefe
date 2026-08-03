using Financ.Domain.Validacoes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes.Categorias
{
    public class CategoriaValidacao : BaseValidacao
    {
        public CategoriaValidacao(string erro) : base(erro){}
        public static void Verifica(bool condicao, string mensagem)
        {
            VerificaExcessao<CategoriaValidacao>(condicao, mensagem);
        }
    }
}
