using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using System.Globalization;

namespace HeyChefe.Domain.Objetos_de_Valor.Titulo
{
    public abstract record TituloBase
    {
        public string Texto { get; }
        protected virtual int TamanhoMinimo { get; } = 2;
        protected abstract int TamanhoMaximo { get; }
        protected TituloBase() { }
        protected TituloBase(string texto)
        {
            ValidaNulo.Verifica(texto, MensagensBase.TITULO_NULO);
            texto = Prepara(texto);
            Valida(texto);
            Texto = texto;
        }
        protected virtual string Prepara(string texto)
        {
            texto = texto.Trim();
            var culturaBR = new CultureInfo("pt-BR");
            texto = culturaBR.TextInfo.ToTitleCase(texto.ToLower());
            return texto;
        }

        protected abstract void Valida(string texto);
    }
}
