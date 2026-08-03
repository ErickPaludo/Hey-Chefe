using Financ.Domain.Validacoes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes.Usuarios
{
    public sealed class UsuariosValidacao : BaseValidacao
    {
        public UsuariosValidacao(string Erro) : base(Erro) { }
        public static void Verifica(bool condicao, string mensagem)
        {
            VerificaExcessao<UsuariosValidacao>(condicao, mensagem);
        }
    }
}
