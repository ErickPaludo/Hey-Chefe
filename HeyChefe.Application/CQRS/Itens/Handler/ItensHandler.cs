using HeyChefe.Application.CQRS.Itens.Query;
using HeyChefe.Application.DTOs.Itens.Get;
using HeyChefe.Application.Mapeamento;
using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Interfaces;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.CQRS.Itens.Handler
{
    public class ItensHandler : IRequestHandler<ItensQuey, ItensDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ItensHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ItensDTO> Handle(ItensQuey request, CancellationToken cancellationToken)
        {
            IEnumerable<Item> itens = await _unitOfWork.itemRepositorio.RetornarTudo();
            return ItemMapper.ParaDTO(itens); 
        }
    }
}
