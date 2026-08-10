using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Segurança.Commands;
using HeyChefe.Application.DTOs.Autenticação.Get;
using HeyChefe.Application.Interfaces.Segurança;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Interfaces;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Segurança.Handler
{
    public class AlterarSenhaHandler : IRequestHandler<AlterarSenhaCommand, Resultado<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISegurancaServico _passService;
        public AlterarSenhaHandler(IUnitOfWork unitOfWork, ISegurancaServico passService)
        {
            _unitOfWork = unitOfWork;
            _passService = passService;
        }
        async Task<Resultado<string>> IRequestHandler<AlterarSenhaCommand, Resultado<string>>.Handle(AlterarSenhaCommand request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _unitOfWork.usuarioRepostorio.BuscarObjetoUnico(x => x.Id.Equals(request.idUsuario));

            if (usuario is null)
                return Resultado<string>.GeraFalha(Falha.NaoEncontrado("Usuário não encontrado!"));

            if (!_passService.ValidaSenhaArgon(usuario.Senha.Hash, request.senhaAntiga, usuario.Senha.Salt))
                return Resultado<string>.GeraFalha(Falha.NaoAutorizado("Senha inválida."));

            var senha = _passService.CriaSenhaArgon(request.senhaNova,null);
            //usuario.Senha.AtualizaSenha(senha.salt,senha.hash);
            //_unitOfWork.usuariosRepostorio.Atualiza(usuario);
            await _unitOfWork.Commit();

            return Resultado<string>.GeraSucesso("Senha alterada com sucesso!");
        }
    }
}
