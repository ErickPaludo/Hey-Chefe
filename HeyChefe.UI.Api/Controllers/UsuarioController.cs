using HeyChefe.Application.CQRS.Segurança.Commands;
using HeyChefe.Application.CQRS.Usuarios.Commands;
using HeyChefe.Application.CQRS.Usuarios.Querys;
using HeyChefe.Application.DTOs.Usuarios.Post;
using HeyChefe.Domain.Entidades;
using HeyChefe.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.SimpleMediator;

namespace HeyChefe.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(CadastraUsuarioDTO usuarioDTO)
        {
            var usuario = await _mediator.Send(new CadastraUsuarioCommand(usuarioDTO.Email, usuarioDTO.PrimeiroNome, usuarioDTO.SegundoNome, usuarioDTO.Senha, usuarioDTO.ConfirmarSenha));
            return Ok(usuario);
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> MeusDados()
        {
            var usuario = await _mediator.Send(new RetornaUsuarioPorIdQuery(User.RetornaIdUsuario()));
            return Ok(usuario);
        }

        [HttpPost("alterar_senha")]
        [Authorize]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaDTO senhaDTO)
        {
            var usuario = await _mediator.Send(new AlterarSenhaCommand(User.RetornaIdUsuario(), senhaDTO.senhaAntiga, senhaDTO.senhaNova));
            return Ok(usuario);
        }
    }
}
