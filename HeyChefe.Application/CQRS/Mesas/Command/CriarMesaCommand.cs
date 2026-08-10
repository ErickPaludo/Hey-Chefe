using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.CQRS.Mesas.Command
{
    public record CriarMesaCommand(int Codigo) : IRequest<string>;
}
