namespace HeyChefe.Application.DTOs.Autenticação.Get
{
    public class RetornaTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiracao { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiracaoRefresh { get; set; }
    }
}
