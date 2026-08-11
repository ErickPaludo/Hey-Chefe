using HeyChefe.Application.Comun.Enums;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Validacoes.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.Services.PermissoesUsuarios
{
    public static class UsuarioExtensao
    {
        public static async Task<Usuario> ValidarUsuario(this IUnitOfWork unitOfWork, Guid usuarioId, PermissaoUsuario permissao)
        {
            Usuario? usuario = await unitOfWork.usuarioRepostorio.BuscarObjetoUnico(x => x.Id == usuarioId);

            if (usuario == null)
                throw new UsuariosValidacao("Usuário não encontrado");

            ValidaPermissao.Valiidar(usuario, PermissaoUsuario.CriarCategoria);

            return usuario;
        }
    }
}
