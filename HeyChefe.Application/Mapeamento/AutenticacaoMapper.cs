using HeyChefe.Application.DTOs.Autenticação.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.Mapeamento
{
    public static class AutenticacaoMapper
    {
        public static RetornaTokenDTO ParaDTO(string email, DateTime token) => new RetornaTokenDTO
        { Token = email, Expiracao = token };
    }
}
