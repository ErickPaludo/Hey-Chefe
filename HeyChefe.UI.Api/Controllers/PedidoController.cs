using HeyChefe.Application.CQRS.Pedidos.Command;
using HeyChefe.Application.CQRS.Pedidos.Query;
using HeyChefe.Application.DTOs.Pedidos.Post;
using HeyChefe.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.SimpleMediator;

namespace HeyChefe.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PedidoController(IMediator mediator) => _mediator = mediator;

        [HttpPost("criar")]
        public async Task<IActionResult> CriarPedido(CriarPedidoDTO pedidoDTO)
        {
            string retorno = await _mediator.Send(new CriarPedidoCommand(User.GetId(), pedidoDTO.MesaId,
                pedidoDTO.Prioridade, pedidoDTO.LinhasPedido));
            return Ok(retorno);
        }

        [HttpGet]
        public async Task<IActionResult> RetornaPedidos() => Ok(await _mediator.Send(new PedidoQuery()));
    }
}