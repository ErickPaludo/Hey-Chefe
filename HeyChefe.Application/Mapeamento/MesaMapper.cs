using HeyChefe.Application.DTOs.Mesas.Get;
using HeyChefe.Domain.Entidades.Mesas;

namespace HeyChefe.Application.Mapeamento;

public static class MesaMapper
{
    public static IEnumerable<MesaDTO> ParaDTO(IEnumerable<Mesa> mesas)
        => mesas.Select(mesa =>
            new MesaDTO(
                mesa.Id,
                mesa.Codigo.Valor,
                mesa.Situacao
            )
        );
}