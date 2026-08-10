using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.DTOs.Base;
using HeyChefe.Application.DTOs.Categoria.Get;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Categorias.Query
{
    public record RetornaCategoriasQuery(int IdConta,string IdUsuario) : IRequest<Resultado<BaseGetList<CategoriaDTO>>>;
}
