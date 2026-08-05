using HeyChefe.Domain.Validacoes.Base;
using HeyChefe.Domain.Validacoes.Item;

namespace HeyChefe.Domain.Validacoes.Mesas
{
    public class MesaValidacao : BaseValidacao
    {
        public MesaValidacao(string erro) : base(erro){}
        public static void Verifica(bool condicao, string mensagem)
        {
            VerificaExcessao<MesaValidacao>(condicao, mensagem);
        }
    }
}
