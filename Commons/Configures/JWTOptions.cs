namespace Commons;

public class JWTOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    /// <summary>
    /// jwt私钥
    /// </summary>
    public string TokenSecret { get; set; }
    /// <summary>
    /// jwt令牌过期时间
    /// </summary>
    public int ExpireSeconds { get; set; }
}
