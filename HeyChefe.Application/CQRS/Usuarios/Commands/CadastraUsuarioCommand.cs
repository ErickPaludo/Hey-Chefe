using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Usuarios.Commands
{
    public record CadastraUsuarioCommand(string Email, string PrimeiroNome, string SegundoNome, string Senha, string ConfirmarSenha) : IRequest<string>;
}
