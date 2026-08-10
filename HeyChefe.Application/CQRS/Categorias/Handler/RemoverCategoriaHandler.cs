using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Categorias.Command;
using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Interfaces;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Categorias.Handler
{
    public class RemoverCategoriaHandler : IRequestHandler<RemoverCategoriaCommand, Resultado<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoverCategoriaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado<string>> Handle(RemoverCategoriaCommand request, CancellationToken cancellationToken)
        {
            //Categoria? categoria = await _unitOfWork.categoriaRepositorio.ObterCategoriaComConta(c => c.Id == request.IdCategoria);

            //if (categoria is null)
            //    return Resultado<string>.GeraFalha(Falha.NaoEncontrado("Categoria não encontrada"));

            //ContaUsuario? contaUsuario = categoria.Conta.ContaUsuarios.FirstOrDefault(c => c.IdUsuario == request.IdUsuario);

            //categoria.Remover(contaUsuario);

            //_unitOfWork.categoriaRepositorio.Delete(categoria);
            //await _unitOfWork.Commit();
            //return Resultado<string>.GeraSucesso("Categoria removida com sucesso");
            return null;
        }
    }
}
