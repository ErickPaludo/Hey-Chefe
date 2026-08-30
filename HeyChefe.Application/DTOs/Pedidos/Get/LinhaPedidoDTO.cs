using HeyChefe.Domain.Entidades.Pedidos.Enums;

namespace HeyChefe.Domain.Consultas.Pedido;

public record LinhaPedidoDTO(ItemLinhaPedidoDTO Item,  ESituacaoLinhaPedido Situacao, bool Cortesia);