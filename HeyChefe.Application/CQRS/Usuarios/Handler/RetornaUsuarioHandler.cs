using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Usuarios.Querys;
using HeyChefe.Application.DTOs.Usuarios.Get;
using HeyChefe.Application.Mapeamento;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Interfaces;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Usuarios.Handler
{
    public class RetornaUsuarioHandler : IRequestHandler<RetornaUsuarioPorIdQuery, Resultado<RetornaUsuarioDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RetornaUsuarioHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Resultado<RetornaUsuarioDTO>> Handle(RetornaUsuarioPorIdQuery request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _unitOfWork.usuarioRepostorio.BuscarPeloId(request.IdUsuario);
            if (usuario == null)
                return Resultado<RetornaUsuarioDTO>.GeraFalha(Falha.NaoEncontrado("Usuário não encontrado."));

            return Resultado<RetornaUsuarioDTO>.GeraSucesso(UsuarioMapper.ParaDTO(usuario));
        }
    }
}
