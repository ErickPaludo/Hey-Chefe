using HeyChefe.Domain.Validacoes.Base;

namespace HeyChefe.Domain.Validacoes.Segurança
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
