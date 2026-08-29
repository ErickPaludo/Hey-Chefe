namespace HeyChefe.Domain.Consultas.Pedido;

public record ItemLinhaPedidoDTO(Guid Id, string Nome, string? Descricao, decimal Valor);