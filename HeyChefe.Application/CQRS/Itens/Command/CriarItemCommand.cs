using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.CQRS.Itens.Command
{
    public record CriarItemCommand(Guid UsuarioId,int Codigo, string Nome, string Descricao, decimal PrecoCusto, decimal MargemLucro, Guid? CategoriaId) : IRequest<string>;
}
