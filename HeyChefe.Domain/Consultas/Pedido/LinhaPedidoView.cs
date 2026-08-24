using HeyChefe.Domain.Entidades.Pedidos.Enums;

namespace HeyChefe.Domain.Consultas.Pedido;

public record LinhaPedidoView(ItemLinhaPedidoView Item, ESituacaoLinhaPedido Situacao, bool Cortesia);