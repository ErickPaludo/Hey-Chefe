using HeyChefe.Application.Interfaces;
using HeyChefe.Application.Comun.Enums;
using HeyChefe.Application.Exceções;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;


namespace HeyChefe.Application.Services.PermissoesUsuarios
{
    public class ValidaPermissao
    {
        public static void Valiidar(Usuario usuario, PermissaoUsuario acao)
        {
            if (usuario.Situacao != ESituacaoUsuario.Ativo)
                throw new ExceptionPermissoes("O usuário não está ativo na conta.");

            if (!ServicoPermiteAcesso.PossuiPermissao(usuario.Permissao, acao))
                throw new ExceptionPermissoes("O usuário não possui permissão para realizar esta ação.");

        }
    }
}
