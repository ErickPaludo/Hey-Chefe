using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Usuarios.Commands
{
    public record CadastraColaboradorCommand(Guid UsuarioId, string Email, string PrimeiroNome, string SegundoNome, string Senha, string ConfirmarSenha) : IRequest<string>;
}
