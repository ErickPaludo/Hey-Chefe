using HeyChefe.Domain.Interfaces.Repositorios.Categorias;
using HeyChefe.Domain.Interfaces.Repositorios.Segurança;
using HeyChefe.Domain.Interfaces.Repositorios.Usuarios;

namespace HeyChefe.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUsuariosRepositorio usuariosRepostorio { get; }
        IAutenticacoesRepositorio autenticacoesRepositorio { get; }
        ICategoriaRepositorio categoriaRepositorio { get; }

        Task Commit();
    }
}
