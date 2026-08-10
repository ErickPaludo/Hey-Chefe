using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.Comun.Resultado
{
    public sealed record Falha(int Codigo,string Mensagem)
    {
        public static Falha NaoEncontrado (string mensagem = "Identificador não encontrado!") => new Falha(404, mensagem);
        public static Falha ErroOperacional (string mensagem = "Erro genérico!") => new Falha(400, mensagem);

        public static Falha NaoAutorizado(string mensagem = "Usuário não autorizado") => new Falha(401, mensagem);
    }
}
