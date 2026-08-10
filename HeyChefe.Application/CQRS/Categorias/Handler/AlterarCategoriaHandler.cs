using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Categorias.Command;
using HeyChefe.Application.DTOs.Base;
using HeyChefe.Application.DTOs.Categoria.Get;
using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Interfaces;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Categorias.Handler
{
    public class AlterarCategoriaHandler : IRequestHandler<AlterarCategoriaCommand, Resultado<BasePost<CategoriaDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlterarCategoriaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Resultado<BasePost<CategoriaDTO>>> Handle(AlterarCategoriaCommand request, CancellationToken cancellationToken)
        {


            //Categoria? categoria = await _unitOfWork.categoriaRepositorio.ObterCategoriaComConta(c => c.Id == request.IdCategoria);

            //if (categoria is null)
            //    return Resultado<BasePost<CategoriaDTO>>.GeraFalha(Falha.NaoEncontrado("Categoria não encontrada"));

            //ContaUsuario? contaUsuario = categoria.Conta.ContaUsuarios.FirstOrDefault(c => c.IdUsuario == request.IdUsuario);

            //categoria.Alterar(contaUsuario, request.Nome, request.Cor);

            //_unitOfWork.categoriaRepositorio.Atualiza(categoria);
            //await _unitOfWork.Commit();
            //return Resultado<BasePost<CategoriaDTO>>.GeraSucesso(new BasePost<CategoriaDTO>(CategoriaMapper.ParaDTO(categoria)));
            return null;
        }
    }
}
