using System.ComponentModel.DataAnnotations;

namespace HeyChefe.Application.DTOs.Usuarios.Post
{
    public class CadastraUsuarioDTO
    {
        public string PrimeiroNome { get; set; }

        public string SegundoNome { get; set; }

        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string ConfirmarSenha { get; set; }
    }
}
