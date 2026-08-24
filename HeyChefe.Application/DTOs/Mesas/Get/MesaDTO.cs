using HeyChefe.Domain.Entidades.Mesas.Enums;

namespace HeyChefe.Application.DTOs.Mesas.Get;

public record MesaDTO(Guid Id,int Codigo,ESituacaoMesa Situacao);