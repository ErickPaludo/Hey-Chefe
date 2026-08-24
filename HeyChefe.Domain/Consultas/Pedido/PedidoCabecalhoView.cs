using HeyChefe.Domain.Consultas.Pedido;
using HeyChefe.Domain.Entidades.Pedidos.Enums;

namespace HeyChefe.Domain.Consultas.Pedido
{
    public record PedidoCabecalhoView(Guid Id, string NumPedido, string Mesa, ESituacaoPedido Situacao, decimal QuantidadeTotalItens, decimal ValorFinal, CriadorPedidoView CriadorPedidoView, List<LinhaPedidoView> LinhasPedido);
}


