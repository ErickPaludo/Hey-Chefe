using HeyChefe.Application.Comun.Enums;
using HeyChefe.Application.CQRS.Usuarios.Commands;
using HeyChefe.Application.Interfaces.Segurança;
using HeyChefe.Application.Services.PermissoesUsuarios;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Segurança;
using HeyChefe.Domain.Validacoes.Usuarios;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Usuarios.Handler
{
    public class CriarColaboradorHandler : IRequestHandler<CadastraColaboradorCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISegurancaServico _passService;
        private readonly IMediator _mediator;
        public CriarColaboradorHandler(IUnitOfWork unitOfWork, ISegurancaServico passService, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _passService = passService;
            _mediator = mediator;
        }
        public async Task<string> Handle(CadastraColaboradorCommand request, CancellationToken cancellationToken)
        {
             await _unitOfWork.ValidarUsuario(request.UsuarioId, PermissaoUsuario.CriarCategoria);

            if (await _unitOfWork.usuarioRepostorio.ExisteId(x => x.Email.Endereco == request.Email.ToLower()))
                throw new UsuariosValidacao("Já existe um usuário cadastrado com esse e-mail.");

            if (request.Senha != request.ConfirmarSenha)
                throw new AutenticacaoValidacao("A senhas não são identicas!");

            var converteSenha = _passService.CriaSenhaArgon(request.Senha);
            NomeUsuario nomeUsuario = NomeUsuario.Create(request.PrimeiroNome, request.SegundoNome);
            Email email = Email.Create(request.Email);
            Senha senha = Senha.Create(converteSenha.salt, converteSenha.hash);

            Usuario usuario = Usuario.Create(nomeUsuario, email, senha, EPermissaoUsuario.Garcom);

            await _unitOfWork.usuarioRepostorio.Adicionar(usuario);
            await _unitOfWork.Commit();
            return "Colaborador criado com sucesso!";
        }
    }
}
