using JF.OrdemServico.Domain.Entities;

namespace JF.OrdemServico.Domain.Interfaces.Services;

public interface IAuthService
{
    Task<Usuario?> AuthenticateAsync(string login, string senha);

    string GenerateJwtToken(Usuario usuario);
}