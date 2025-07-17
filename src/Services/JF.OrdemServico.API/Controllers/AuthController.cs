using JF.OrdemServico.API.DTOs.Request.Login;
using JF.OrdemServico.API.DTOs.Response.Login;
using JF.OrdemServico.Domain.Interfaces.Services;
using JF.OrdemServico.Services.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JF.OrdemServico.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IServiceProvider provider, IAuthService authService) : base(provider)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var usuario = await _authService.AuthenticateAsync(request.Login, request.Senha);

        if (usuario == null)
        {
            return ResponseFail("Login ou senha incorretos", HttpStatusCode.Unauthorized);
        }

        var token = _authService.GenerateJwtToken(usuario);
        var loginResponse = _mapper.Map<LoginResponse>((usuario, token));

        return ResponseResult(loginResponse, "Login realizado com sucesso");
    }
}