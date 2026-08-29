using HeyChefe.Domain.Consultas.Pedido;

namespace HeyChefe.Application.Interfaces.Repository;

public interface IPedidoAppRepository
{
    Task<IEnumerable<PedidosDTO>> SelecionaPedidos();
}