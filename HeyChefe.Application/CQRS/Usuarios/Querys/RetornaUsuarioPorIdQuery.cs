using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.DTOs.Usuarios.Get;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Usuarios.Querys
{
    public class RetornaUsuarioPorIdQuery : IRequest<Resultado<RetornaUsuarioDTO>>
    {
        public Guid IdUsuario { get; private set; }
        public RetornaUsuarioPorIdQuery(Guid idUsuario)
        {
            IdUsuario = idUsuario;
        }
    }
}
