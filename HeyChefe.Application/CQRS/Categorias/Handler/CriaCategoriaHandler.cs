using HeyChefe.Application.Comun.Enums;
using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Categorias.Command;
using HeyChefe.Application.DTOs.Base;
using HeyChefe.Application.DTOs.Categoria.Get;
using HeyChefe.Application.Services.PermissoesUsuarios;
using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes.Codigo;
using HeyChefe.Domain.Validacoes.Segurança;
using NetDevPack.SimpleMediator;


namespace HeyChefe.Application.CQRS.Categorias.Handler
{
    public class CriaCategoriaHandler : IRequestHandler<CriaCategoriaCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CriaCategoriaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CriaCategoriaCommand request, CancellationToken cancellationToken)
        {
            Usuario usuario = await _unitOfWork.ValidarUsuario(request.UsuarioId, PermissaoUsuario.CriarCategoria);

            //Verificar se codigo existe
            if (await _unitOfWork.categoriaRepositorio.BuscarObjetoUnico(x => x.Codigo.Valor == request.Codigo) != null)
                throw new CodigoValidacao("Código já existe");
            //criar codigo
            Codigo codigo = Codigo.Create(request.Codigo);
            //criar titulo
            TituloCategoria titulo = TituloCategoria.Create(request.Nome);

            //cria categoria
            Categoria categoria = 
                request.Cor is null ?
                Categoria.Create(codigo,titulo) :
                Categoria.Create(codigo,titulo,Cor.Create(request.Cor));

            await _unitOfWork.categoriaRepositorio.Adicionar(categoria);
            await _unitOfWork.Commit();

            return "Categoria criada.";
        }
    }
}
