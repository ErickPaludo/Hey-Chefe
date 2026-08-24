namespace HeyChefe.Domain.Consultas.Pedido;

public record ItemLinhaPedidoView(Guid Id, string Nome, string? Descricao, decimal Valor);