using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.CQRS.Mesas.Command
{
    public record CriarMesaCommand(Guid UsuarioId, int Codigo) : IRequest<string>;
}
