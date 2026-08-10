using HeyChefe.Application.CQRS.Autenticação.Commands;
using HeyChefe.Application.DTOs.Autenticação.Post;
using HeyChefe.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.SimpleMediator;

namespace HeyChefe.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AutenticacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ConectaUsuarioDTO usuario)
        {
            var tokenAutenticacao = await _mediator.Send(new AutenticacaoCommand(usuario.Email, usuario.Senha));

            SetRefreshTokenCookie(tokenAutenticacao.RefreshToken);

            return Ok(tokenAutenticacao);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var tokenAutenticacao = await _mediator.Send(new RefreshTokenCommand(refreshToken));

            SetRefreshTokenCookie(tokenAutenticacao.RefreshToken);

            return Ok(tokenAutenticacao);
        }
        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            var tokenAutenticacao = await _mediator.Send(new RevokeCommand(User.RetornaIdUsuario()));
            return Ok(tokenAutenticacao);
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken)) return;

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // OBRIGATÓRIO PARA HTTPS
                SameSite = SameSiteMode.None, // OBRIGATÓRIO PARA CROSS-DOMAIN
                Expires = DateTime.UtcNow.AddDays(7),
                IsEssential = true
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
