using HeyChefe.Application.CQRS.Itens.Command;
using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Objetos_de_Valor.Observação;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes.Categorias;
using HeyChefe.Domain.Validacoes.Codigo;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.CQRS.Itens.Handler
{
    public class CriarItemHandler : IRequestHandler<CriarItemCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CriarItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CriarItemCommand request, CancellationToken cancellationToken)
        {
            //Validar se categoria existe
            Categoria? categoria = null;
            if (request.CategoriaId.HasValue)
            {
                categoria = await _unitOfWork.categoriaRepositorio.BuscarObjetoUnico(x => x.Id == request.CategoriaId.Value);
                if (categoria == null) { throw new CategoriaValidacao("Categoria não encontrada"); }
            }

            //Cria Codigo
            if (await _unitOfWork.itemRepositorio.BuscarObjetoUnico(x => x.Codigo.Valor == request.Codigo) != null)
                throw new CodigoValidacao("Código já existe");
            Codigo codigo = Codigo.Create(request.Codigo);

            //Criar TituloItem
            TituloItem nome = TituloItem.Create(request.Nome);

            //Criar ObservacaoItem
            ObservacaoItem observacao = ObservacaoItem.Create(request.Descricao);

            //Criar Saldo
            Saldo precoCusto = Saldo.Create(request.PrecoCusto);

            //Criar Lucro
            Saldo margemLucro = Saldo.Create(request.MargemLucro);

            //Criar Item
            Item item = Item.Create(codigo, observacao, nome, precoCusto, margemLucro, categoria);

            //Grava no banco
            await _unitOfWork.itemRepositorio.Adicionar(item);
            await _unitOfWork.Commit();
            
            return $"Item criado com sucesso";
        }
    }
}
