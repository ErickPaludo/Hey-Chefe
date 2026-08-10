using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Entidades.Itens.Enums;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Objetos_de_Valor.Observação;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes;
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
        public TituloItem Nome { get; private set; }
        public ObservacaoItem? Descricao { get; private set; }
        public Saldo PrecoCusto { get; private set; }
        public Saldo MargemLucro { get; private set; }
        public Saldo PrecoFinal => PrecoCusto.Porcentagem(MargemLucro);
        public Categoria? Categoria { get; private set; }
        public ESituacaoItem Situacao { get; set; }

        public Item() { }
        private Item(Codigo codigo, ObservacaoItem? descricao, TituloItem nome, Saldo precoVenda, Saldo margemLucro,Categoria? categoria)
        {
            ValidaNulo.Verifica(codigo, MensagensBase.CODIGO_OBRIGATORIO);
            ValidaNulo.Verifica(nome, MensagensBase.CODIGO_OBRIGATORIO);

            ValidaPreco(precoVenda);
            ValidaMargemLucro(margemLucro);

            Codigo = codigo;
            Nome = nome;
            Descricao = descricao;
            PrecoCusto = precoVenda;
            MargemLucro = margemLucro;
            Categoria = categoria;
            Situacao = ESituacaoItem.Ativo;
        }
        public static Item Create(Codigo codigo, ObservacaoItem? descricao, TituloItem nome, Saldo precoVenda, Saldo margemLucro,Categoria? categoria) =>
            new Item(codigo, descricao, nome, precoVenda, margemLucro,categoria);

        private void ValidaPreco(Saldo preco)
        {
            ValidaNulo.Verifica(preco, MensagensBase.PRECO_VENDA_NULO);
            ItemValidacao.Verifica(preco.Valor <= 0, MensagemItem.VALOR_INVALIDO);
        }
        private void ValidaMargemLucro(Saldo margemLucro)
        {
            ValidaNulo.Verifica(margemLucro, MensagensBase.MARGEM_LUCRO_NULA);
            ItemValidacao.Verifica(margemLucro.Valor < 0, MensagemItem.MARGEM_LUCRO_INVALIDA);
        }
        #region Atualiza
        public void AtualizarNome(TituloItem nome)
        {
            ValidaNulo.Verifica(nome, MensagensBase.CODIGO_OBRIGATORIO);
            Nome = nome;
            DataHoraAlteracao = DateTime.UtcNow;
        }
        public void AtualizarDescricao(ObservacaoItem? descricao)
        {
            Descricao = descricao;
            DataHoraAlteracao = DateTime.UtcNow;
        }
        public void AtualizarPrecoVenda(Saldo precoVenda)
        {
            ValidaPreco(precoVenda);
            PrecoCusto = precoVenda;
            DataHoraAlteracao = DateTime.UtcNow;
        }
        public void AtualizarMargemLucro(Saldo margemLucro)
        {
            ValidaMargemLucro(margemLucro);
            MargemLucro = margemLucro;
            DataHoraAlteracao = DateTime.UtcNow;
        }
        public void AtualizarCategoria(Categoria? categoria)
        {
            Categoria = categoria;
            DataHoraAlteracao = DateTime.UtcNow;
        }
        public void AtualizarSituacao(ESituacaoItem situacao)
        {
            ItemValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoItem), situacao), MensagensBase.SITUACAO_INVALIDA);
            Situacao = situacao;
            DataHoraAlteracao = DateTime.UtcNow;
        }
        #endregion
    }
}
