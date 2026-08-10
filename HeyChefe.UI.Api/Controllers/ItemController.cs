using HeyChefe.Application.CQRS.Itens.Command;
using HeyChefe.Application.CQRS.Itens.Query;
using HeyChefe.Application.DTOs.Itens.Get;
using HeyChefe.Application.DTOs.Itens.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.SimpleMediator;

namespace HeyChefe.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("criar")]
        public async Task<IActionResult> CriarItem([FromBody] CriarItemDTO itemDTO)
        {
            string retorno = await _mediator.Send(new CriarItemCommand(itemDTO.Codigo,itemDTO.Nome,itemDTO.Descricao,itemDTO.PrecoCusto,itemDTO.MargemLucro,itemDTO.CategoriaId));
            return Ok(retorno);
        }
        [HttpGet]
        public async Task<IActionResult> RetorarItens()
        {
             ItensDTO itensDTO = await _mediator.Send(new ItensQuey());
             return Ok(itensDTO);
        }
    }
}
