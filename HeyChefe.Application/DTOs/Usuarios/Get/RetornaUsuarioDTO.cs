using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.DTOs.Usuarios.Get
{
    public record RetornaUsuarioDTO(
        Guid Id,
        string PrimeiroNome,
        string SegundoNome,
        string NomeCompleto,
        string Email);
    
}
