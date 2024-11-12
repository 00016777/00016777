namespace Restaurant.Application.Models.JWTSettings
{
    public class TokenModel
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
