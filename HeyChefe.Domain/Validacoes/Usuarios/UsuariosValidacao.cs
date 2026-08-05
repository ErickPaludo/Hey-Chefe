using HeyChefe.Domain.Validacoes.Base;

namespace HeyChefe.Domain.Validacoes.Usuarios
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
