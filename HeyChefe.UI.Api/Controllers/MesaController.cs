using HeyChefe.Application.CQRS.Mesas.Command;
using HeyChefe.Application.DTOs.Mesas.Post;
using HeyChefe.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.SimpleMediator;

namespace HeyChefe.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MesaController(IMediator mediator) => _mediator = mediator;
        [HttpPost]
        public async Task<IActionResult> CriarMes(CriarMesaDTO criarMesaDTO)
        {
            string retorno = await _mediator.Send(new CriarMesaCommand(User.GetId(),criarMesaDTO.Codigo));
            return Ok(retorno);
        }
    }
}
