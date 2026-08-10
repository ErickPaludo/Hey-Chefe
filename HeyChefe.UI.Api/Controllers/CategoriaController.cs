using HeyChefe.Application.CQRS.Categorias.Command;
using HeyChefe.Application.CQRS.Categorias.Query;
using HeyChefe.Application.DTOs.Categoria.Post;
using HeyChefe.Application.DTOs.Categorias.Patch;
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
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CriarCategoria(CadastraCategoriaDTO categoriaDTO)
        {
            var categoria = await _mediator.Send(new CriaCategoriaCommand(categoriaDTO.Codigo, categoriaDTO.Nome, categoriaDTO.cor));
            return Ok(categoria);
        }
        //[HttpGet]
        //public async Task<IActionResult> RetornaCategorias(int idConta)
        //{
        //    var categoria = await _mediator.Send(new RetornaCategoriasQuery(idConta, User.RetornaIdUsuario()));
        //    return categoria.RetornoAutomatico();
        //}
        //[HttpPatch("/api/Contas/Categorias/{idCategoria}/Alterar")]
        //public async Task<IActionResult> AlterarCategoria(int idCategoria, [FromBody] AlterarCategoriaDTO categoriaDTO )
        //{
        //    var categoria = await _mediator.Send(new AlterarCategoriaCommand(idCategoria, User.RetornaIdUsuario(),categoriaDTO.Nome,categoriaDTO.Cor));
        //    return categoria.RetornoAutomatico();
        //}
        //[HttpDelete("/api/Contas/Categorias/{idCategoria}/Remover")]
        //public async Task<IActionResult> RemoverCategoria(int idCategoria)
        //{
        //    var categoria = await _mediator.Send(new RemoverCategoriaCommand(idCategoria, User.RetornaIdUsuario()));
        //    return categoria.RetornoAutomatico();
        //}
    }
}
