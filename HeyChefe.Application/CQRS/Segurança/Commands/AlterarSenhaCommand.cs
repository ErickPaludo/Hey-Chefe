using HeyChefe.Application.Comun.Resultado;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Segurança.Commands
{
    public record class AlterarSenhaCommand(Guid idUsuario, string senhaAntiga, string senhaNova) : IRequest<Resultado<string>>;  
}
