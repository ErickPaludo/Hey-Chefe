using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Autenticação.Commands;
using HeyChefe.Application.Interfaces;
using HeyChefe.Domain.Entidades.Segurança;
using HeyChefe.Domain.Interfaces;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Autenticação.Handler
{
    public class RevokeHandler : IRequestHandler<RevokeCommand, Resultado<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RevokeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado<string>> Handle(RevokeCommand request, CancellationToken cancellationToken)
        {
            Autenticacao? auth = await _unitOfWork.autenticaoRepositorio.BuscarAuthComUsuarios(x => x.Usuario.Id== request.idUsuario);

            if(auth is null) 
                return Resultado<string>.GeraFalha(Falha.NaoEncontrado("Usuário não encontrado"));

            auth.RevokaToken();
            _unitOfWork.autenticaoRepositorio.Atualiza(auth);
            await _unitOfWork.Commit();
            return Resultado<string>.GeraSucesso("");
        }
    }
}
