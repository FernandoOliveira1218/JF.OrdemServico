using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JF.OrdemServico.Infra.Authentication;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository, JwtSettings jwtOptions)
    {
        _usuarioRepository = usuarioRepository;
        _jwtSettings = jwtOptions;
    }

    public async Task<Usuario?> AuthenticateAsync(string login, string senha)
    {
        var usuario = await _usuarioRepository.GetByLoginAsync(login);
        if (usuario == null)
        {
            // Usuario incorreto, retorna null
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
        {
            // Usuario ou Senha incorreta, retorna null
            return null;
        }

        return usuario;
    }

    public string GenerateJwtToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new(JwtRegisteredClaimNames.Name, usuario.Nome),
                new(JwtRegisteredClaimNames.Email, usuario.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        // Adiciona uma claim por cliente
        if (usuario.ClienteUsuarios != null && usuario.ClienteUsuarios.Any())
        {
            foreach (var cliente in usuario.ClienteUsuarios)
            {
                claims.Add(new Claim("custom:cliente_id", cliente.ClienteId.ToString()));
            }
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
