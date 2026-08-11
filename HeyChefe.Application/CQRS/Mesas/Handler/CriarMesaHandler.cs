using HeyChefe.Application.Comun.Enums;
using HeyChefe.Application.CQRS.Mesas.Command;
using HeyChefe.Application.Services.PermissoesUsuarios;
using HeyChefe.Domain.Entidades.Mesas;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Codigo;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Mesas.Handler
{
    public class CriarMesaHandler : IRequestHandler<CriarMesaCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CriarMesaHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<string> Handle(CriarMesaCommand request, CancellationToken cancellationToken)
        {
            Usuario usuario = await _unitOfWork.ValidarUsuario(request.UsuarioId, PermissaoUsuario.CriarCategoria);

            if (await _unitOfWork.mesaRepository.BuscarObjetoUnico(x => x.Codigo.Valor == request.Codigo) != null)
                throw new CodigoValidacao("Código já existe");

            Codigo codigo = Codigo.Create(request.Codigo);

            Mesa mesa = Mesa.Create(codigo);

            await _unitOfWork.mesaRepository.Adicionar(mesa);
            await _unitOfWork.Commit();
            return $"Mesa {codigo.Valor} criada";
        }
    }
}
