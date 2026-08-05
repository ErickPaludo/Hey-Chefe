using HeyChefe.Domain.Validacoes.Base;

namespace HeyChefe.Domain.Validacoes.Codigo
{
    public class CodigoValidacao : BaseValidacao
    {
        public CodigoValidacao(string erro) : base(erro) { }

        public static void Verifica(bool condicao, string mensagem)
        {
            VerificaExcessao<CodigoValidacao>(condicao, mensagem);
        }
    }
}
