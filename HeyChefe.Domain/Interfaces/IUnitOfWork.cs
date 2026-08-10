using HeyChefe.Domain.Interfaces.Repositorios.Categorias;
using HeyChefe.Domain.Interfaces.Repositorios.Itens;
using HeyChefe.Domain.Interfaces.Repositorios.Mesas;
using HeyChefe.Domain.Interfaces.Repositorios.Pedidos;
using HeyChefe.Domain.Interfaces.Repositorios.Segurança;
using HeyChefe.Domain.Interfaces.Repositorios.Usuarios;

namespace HeyChefe.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUsuariosRepositorio usuarioRepostorio { get; }
        ICategoriaRepositorio categoriaRepositorio { get; }
        IItemRepositorio itemRepositorio { get; }
        IMesaRepository mesaRepository { get; }
        IPedidoRepository pedidoRepository { get; }
        ILinhaPedidoRepository linhaPedidoRepository { get; }
        IAutenticacoesRepositorio autenticaoRepositorio { get; }

        Task Commit();
    }
}
