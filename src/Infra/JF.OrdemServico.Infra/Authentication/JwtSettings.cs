namespace JF.OrdemServico.Infra.Authentication;

public class JwtSettings
{
    public JwtSettings(string? secretKey, string? issuer, string? audience, int? expirationMinutes)
    {
        SecretKey = secretKey ?? string.Empty;
        Issuer = issuer ?? string.Empty;
        Audience = audience ?? string.Empty;
        ExpirationMinutes = expirationMinutes ?? 0;
    }

    public string SecretKey { get; private set; } = string.Empty;

    public string Issuer { get; private set; } = string.Empty;

    public string Audience { get; private set; } = string.Empty;

    public int ExpirationMinutes { get; private set; }
}