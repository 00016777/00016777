﻿namespace Restaurant.Application.Models.JWTSettings;

public class JWTSetting
{
    public JWT JWT { get; set; }
}

public class JWT
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string Secret { get; set; }
}
