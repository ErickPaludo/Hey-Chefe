using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Categorias.Query;
using HeyChefe.Application.DTOs.Base;
using HeyChefe.Application.DTOs.Categoria.Get;
using HeyChefe.Domain.Interfaces;
using NetDevPack.SimpleMediator;


namespace HeyChefe.Application.CQRS.Categorias.Handler
{
    internal class RetornaCategoriasHandler : IRequestHandler<RetornaCategoriasQuery, Resultado<BaseGetList<CategoriaDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RetornaCategoriasHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Resultado<BaseGetList<CategoriaDTO>>> Handle(RetornaCategoriasQuery request, CancellationToken cancellationToken)
        {
            //Conta? conta = await _unitOfWork.contasRepositorio.BuscarContaComUsuarios(x => x.Id == request.IdConta);

            //if (conta is null)
            //    return Resultado<BaseGetList<CategoriaDTO>>.GeraFalha(Falha.NaoEncontrado("Conta não encontrada!"));

            //if (!conta.ContaUsuarios.Any(x => x.IdUsuario == request.IdUsuario))
            //    return Resultado<BaseGetList<CategoriaDTO>>.GeraFalha(Falha.ErroOperacional("Usuário não pertence a está conta."));

            //var categorias = await _unitOfWork.categoriaRepositorio.BuscarPorCondicao(x => x.IdConta == request.IdConta);
            //return Resultado<BaseGetList<CategoriaDTO>>.GeraSucesso(new BaseGetList<CategoriaDTO>(CategoriaMapper.ParaListDTO(categorias.OrderBy(x => x.Nome))));
            return null;
        }
    }
}
