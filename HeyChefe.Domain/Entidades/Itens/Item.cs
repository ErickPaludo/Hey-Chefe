using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Entidades.Itens.Enums;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Objetos_de_Valor.Observação;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Item;
using HeyChefe.Domain.Validacoes.Item.Mensagens;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;

namespace HeyChefe.Domain.Entidades.Itens
{
    public sealed class Item : EntidadeBase
    {
        public Codigo Codigo { get; private set; }
        public ObservacaoItem Descricao { get; private set; }
        public TituloItem Nome { get; private set; }
        public Saldo PrecoVenda { get; private set; }
        public Saldo MargemLucro { get; private set; }
        public Saldo PrecoFinal => PrecoVenda.Porcentagem(MargemLucro);
        public Categoria? Categoria { get; private set; }
        public ESituacaoItem Situacao { get; set; }

        private Item(Codigo codigo, ObservacaoItem descricao, TituloItem nome, Saldo precoVenda, Saldo margemLucro)
        {
            ValidaPreco(precoVenda);
            ValidaMargemLucro(margemLucro);

            Codigo = codigo;
            Nome = nome;
            Descricao = descricao;
            PrecoVenda = precoVenda;
            MargemLucro = margemLucro;
            Situacao = ESituacaoItem.Ativo;
        }
        public static Item Create(Codigo codigo, ObservacaoItem descricao, TituloItem nome, Saldo precoVenda, Saldo margemLucro) =>
            new Item(codigo, descricao, nome, precoVenda, margemLucro);
        
        private void ValidaPreco(Saldo preco) => ItemValidacao.Verifica(preco.Valor <= 0, MensagemItem.VALOR_INVALIDO);
        private void ValidaMargemLucro(Saldo margemLucro) => ItemValidacao.Verifica(margemLucro.Valor < 0, MensagemItem.MARGEM_LUCRO_INVALIDA);
        #region Atualiza
        public void AtualizarNome(TituloItem nome)
        {
            Nome = nome;
        }
        public void AtualizarDescricao(ObservacaoItem descricao)
        {
            Descricao = descricao;
        }
        public void AtualizarPrecoVenda(Saldo precoVenda)
        {
            ValidaPreco(precoVenda);
            PrecoVenda = precoVenda;
        }
        public void AtualizarMargemLucro(Saldo margemLucro)
        {
            ValidaMargemLucro(margemLucro);
            MargemLucro = margemLucro;
        }
        public void AtualizarCategoria(Categoria? categoria)
        {
            Categoria = categoria;
        }
        public void AtualizarSituacao(ESituacaoItem situacao)
        {
            ItemValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoItem), situacao), MensagensBase.SITUACAO_INVALIDA);
            Situacao = situacao;
        }
        #endregion
    }
}
