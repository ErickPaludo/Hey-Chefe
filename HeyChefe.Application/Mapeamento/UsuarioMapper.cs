using HeyChefe.Application.DTOs.Usuarios.Get;
using HeyChefe.Domain.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.Mapeamento
{
    public static class UsuarioMapper
    {
        public static RetornaUsuarioDTO ParaDTO(Usuario usuario) => new RetornaUsuarioDTO(usuario.Id, usuario.Nome.Primeiro, usuario.Nome.Segundo, usuario.Nome.Completo, usuario.Email.Endereco);
    }
}
