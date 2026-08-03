using Financ.Domain.Entidades.ContasBancarias;
using Financ.Domain.Enums.ContasBancarias;
using Financ.Domain.Objetos_de_Valor;
using Financ.Domain.Objetos_de_Valor.Titulo;
using Financ.Domain.Validacoes.Categorias;
using Financ.Domain.Validacoes.Categorias.Mensagens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Entidades.Categorias
{
    public class Categoria : EntidadeBase
    {
        public TituloCategoria Titulo { get; private set; }
        //public int IdConta { get; private set; }
        //public Cor Cor { get; private set; }
        //public Conta Conta { get; private set; }
        //private Categoria() { }
        //public Categoria(ContaUsuario contaUsuario, string nome, string? cor)
        //{

        //    IdConta = contaUsuario.Conta.Id;
        //    Conta = contaUsuario.Conta;
        //    ValidaNome(nome);
        //    Nome = nome;
        //    Cor = new Cor(cor);
        //}
        //public void Remover(ContaUsuario contaUsuario)
        //{
        //    //Não validamos nada até o momento
        //}

        //public void Alterar(ContaUsuario contaUsuario, string? nome, string? cor)
        //{
        //    if (nome is not null)
        //    {
        //        ValidaNome(nome);
        //        Nome = nome.Trim();
        //    }
        //    if (cor is not null)
        //    {
        //        Cor = new Cor(cor);
        //    }
        //}
        //private void ValidaNome(string valor)
        //{
        //    CategoriaValidacao.Verifica(string.IsNullOrWhiteSpace(valor), MensagemCategoria.NOME_OBRIGATORIO);
        //    CategoriaValidacao.Verifica(valor.Length < 2 || valor.Length > 50, MensagemCategoria.CARACTERES_INVALIDOS);
        //}
    }
}
