using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.DTOs.Usuarios.Post
{
    public record AlterarSenhaDTO(string senhaAntiga, string senhaNova);
}
