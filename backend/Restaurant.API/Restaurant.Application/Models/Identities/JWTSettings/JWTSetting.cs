namespace Restaurant.Application.Models.Identities.JWTSettings;

public class JWTSetting
{
    public JWT JWT { get; set; }
}

public class JWT
{
    public string ValidAudience { get; set; } = string.Empty;
    public string ValidIssuer { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
}
