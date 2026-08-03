using Financ.Domain.Validacoes.Base.Mensagens;
using Financ.Domain.Validacoes.Categorias.Mensagens;
using Financ.Domain.Validacoes.ContasBancarias;
using Financ.Domain.Validacoes.ContasBancarias.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Financ.Domain.Objetos_de_Valor.Titulo
{
    public sealed record TituloCategoria : TituloBase
    {
        protected override int TamanhoMaximo => 30;
        private TituloCategoria(string texto) : base(texto){}
        public static TituloCategoria Create(string texto) => new(texto);
        protected override void Valida(string texto)
        {
            ContasValidacao.Verifica(texto.Length < TamanhoMinimo || texto.Length > TamanhoMaximo, MensagensBase.TITULO_TAMANHO_INVALIDO(TamanhoMinimo,TamanhoMaximo));
        }
    }
}
