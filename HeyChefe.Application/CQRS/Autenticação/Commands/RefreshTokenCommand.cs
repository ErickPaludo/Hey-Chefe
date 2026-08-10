using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.DTOs.Autenticação.Get;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Autenticação.Commands
{
    public record RefreshTokenCommand(string? refreshToken) : IRequest<RetornaTokenDTO>;
}
