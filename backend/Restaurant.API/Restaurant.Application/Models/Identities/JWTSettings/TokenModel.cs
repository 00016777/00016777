namespace Restaurant.Application.Models.Identities.JWTSettings
{
    public class TokenModel
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }
}
