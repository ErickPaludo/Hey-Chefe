using HeyChefe.Domain.Validacoes.Base;

namespace HeyChefe.Domain.Validacoes.Cor
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
