using Financ.Domain.Validacoes;
using Financ.Domain.Validacoes.Base.Mensagens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Financ.Domain.Objetos_de_Valor.Titulo
{
    public abstract record TituloBase
    {
        public string Texto { get; }
        protected virtual int TamanhoMinimo { get; } = 2;
        protected abstract int TamanhoMaximo { get; }
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
