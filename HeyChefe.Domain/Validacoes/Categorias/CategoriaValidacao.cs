using HeyChefe.Domain.Validacoes.Base;

namespace HeyChefe.Domain.Validacoes.Categorias
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
