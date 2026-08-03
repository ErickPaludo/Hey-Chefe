using System;
using System.Collections.Generic;
using System.Text;

namespace Financ.Domain.Objetos_de_Valor
{
    public abstract record ObservacaoBase
    {
        public virtual int TamanhoMaximo { get; } = 400;
        public string Texto { get; private set; }

        protected ObservacaoBase(string texto)
        {
            if(!string.IsNullOrEmpty(texto)) texto = Prepara(texto);
            Valida(texto);
            Texto = texto;
        }

        public abstract void Valida(string texto);
        private string Prepara(string texto)
        {
            return texto.Trim();
        }

    }
}
