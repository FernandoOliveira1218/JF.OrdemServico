namespace JF.OrdemServico.API.DTOs.Response.Login;

public record LoginResponse
{
    public Guid? UsuarioId { get; init; }
    public string? Nome { get; init; }
    public string? Email { get; init; }
    public List<Guid>? ClienteIds { get; init; }
    public string? Token { get; init; }

    // Obrigatório para o AutoMapper
    public LoginResponse() { }
}
