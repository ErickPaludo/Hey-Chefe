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
    public class AutenticacaoCommand : IRequest<RetornaTokenDTO>
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public AutenticacaoCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
