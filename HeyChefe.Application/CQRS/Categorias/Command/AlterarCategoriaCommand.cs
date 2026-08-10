using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.DTOs.Base;
using HeyChefe.Application.DTOs.Categoria.Get;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Categorias.Command
{
    public record AlterarCategoriaCommand(int IdCategoria, string IdUsuario, string? Nome, string? Cor) : IRequest<Resultado<BasePost<CategoriaDTO>>>;
}
