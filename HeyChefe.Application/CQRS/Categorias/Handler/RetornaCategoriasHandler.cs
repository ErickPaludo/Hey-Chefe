using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Categorias.Query;
using HeyChefe.Application.DTOs.Base;
using HeyChefe.Application.DTOs.Categoria.Get;
using HeyChefe.Application.DTOs.Categorias.Get;
using HeyChefe.Application.Mapeamento;
using HeyChefe.Domain.Interfaces;
using NetDevPack.SimpleMediator;


namespace HeyChefe.Application.CQRS.Categorias.Handler
{
    internal class RetornaCategoriasHandler : IRequestHandler<RetornaCategoriasQuery, CategoriasDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RetornaCategoriasHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoriasDTO> Handle(RetornaCategoriasQuery request, CancellationToken cancellationToken)
        {
            var categorias = await _unitOfWork.categoriaRepositorio.RetornarTudo();
            return new CategoriasDTO(CategoriaMapper.ParaListDTO(categorias));
        }
    }
}
