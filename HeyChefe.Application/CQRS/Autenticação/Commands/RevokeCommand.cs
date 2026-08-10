using HeyChefe.Application.Comun.Resultado;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Autenticação.Commands
{
    public record RevokeCommand(Guid idUsuario) : IRequest<Resultado<string>>;
}
