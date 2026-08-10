using HeyChefe.Application.Comun.Resultado;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Usuarios.Commands
{
    public class CadastraUsuarioCommand : IRequest<Resultado<string>>
    {
        public string Email { get; set; }
        public string PrimeiroNome { get; set; }
        public string SegundoNome { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
        public CadastraUsuarioCommand(string email,string primeiroNome,string segundoNome, string senha, string confirmarSenha)
        {
            Email = email;
            PrimeiroNome = primeiroNome;
            SegundoNome = segundoNome;
            Senha = senha;
            ConfirmarSenha = confirmarSenha;
        }
    }
}
