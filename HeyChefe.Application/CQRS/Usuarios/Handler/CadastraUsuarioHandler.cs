using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Usuarios.Commands;
using HeyChefe.Application.Interfaces.Segurança;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Interfaces.Repositorios;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Usuarios;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Usuarios.Handler
{
    public class CadastraUsuarioHandler : IRequestHandler<CadastraUsuarioCommand, Resultado<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISegurancaServico _passService;
        public CadastraUsuarioHandler(IUnitOfWork unitOfWork, ISegurancaServico passService)
        {
            _unitOfWork = unitOfWork;
            _passService = passService;
        }
        public async Task<Resultado<string>> Handle(CadastraUsuarioCommand request, CancellationToken cancellationToken)
        {
            request.Email = request.Email.Trim();

            if (await _unitOfWork.usuarioRepostorio.ExisteId(x => x.Email.Endereco == request.Email))
                throw new UsuariosValidacao("Já existe um usuário cadastrado com esse e-mail.");

            if (request.Senha != request.ConfirmarSenha)
                return Resultado<string>.GeraFalha(Falha.ErroOperacional("A senhas não são identicas!")); //Não é bom validar nessa camada, deverá alterar futuramente

            var converteSenha = _passService.CriaSenhaArgon(request.Senha);
            NomeUsuario nomeUsuario = NomeUsuario.Create(request.PrimeiroNome, request.SegundoNome);
            Email email = Email.Create(request.Email);
            Senha senha = Senha.Create(converteSenha.salt, converteSenha.hash);

            Usuario usuario = Usuario.Create(nomeUsuario, email, senha,EPermissaoUsuario.Administrador);

            await _unitOfWork.usuarioRepostorio.Adicionar(usuario);
            await _unitOfWork.Commit();
            return Resultado<string>.GeraSucesso("Usuário criado com sucesso!");
        }
    }
}
