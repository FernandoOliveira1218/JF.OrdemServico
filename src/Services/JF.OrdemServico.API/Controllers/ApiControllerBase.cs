using AutoMapper;
using JF.OrdemServico.API.DTOs.Response;
using JF.OrdemServico.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JF.OrdemServico.Services.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly IMapper _mapper;
    protected readonly NotificationContext _notificationContext;


    protected ApiControllerBase(IServiceProvider provider)
    {
        _mapper = provider.GetRequiredService<IMapper>();
        _notificationContext = provider.GetRequiredService<NotificationContext>();
    }

    protected IActionResult ResponseResult<T>(T? dto, string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        if (!OperacaoValida())
        {
            return ResponseNotificationErrors();
        }
            
        if (dto == null)
        {
            var response = ApiResponse<T>.Fail("Recurso não encontrado ou inválido", HttpStatusCode.NotFound);
            return NotFound(response);
        }

        var responseOk = ApiResponse<T>.Ok(dto, message ?? "Operação realizada com sucesso");

        return statusCode == HttpStatusCode.OK ? Ok(responseOk) : StatusCode((int)statusCode, responseOk);
    }

    protected IActionResult ResponseCreated<T>(T dto, string? message = null)
    {
        if (!OperacaoValida())
        {
            return ResponseNotificationErrors();
        }

        var response = ApiResponse<T>.Created(dto, message ?? "Criado com sucesso");
        return Created(string.Empty, response);
    }

    protected IActionResult ResponseDeleted(string? message = null)
    {
        if (!OperacaoValida())
        {
            return ResponseNotificationErrors();
        }

        var response = ApiResponse<object>.Ok(null, message ?? "Remoção realizada com sucesso");
        return Ok(response);
    }

    protected IActionResult ResponseFail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        var response = ApiResponse<object>.Fail(message, statusCode);

        return statusCode switch
        {
            HttpStatusCode.BadRequest => BadRequest(response),
            HttpStatusCode.NotFound => NotFound(response),
            HttpStatusCode.Unauthorized => Unauthorized(response),
            HttpStatusCode.Forbidden => Forbid(),
            _ => StatusCode((int)statusCode, response)
        };
    }

    protected bool OperacaoValida() => !_notificationContext.HasNotifications;

    private IActionResult ResponseNotificationErrors()
    {
        var erros = _notificationContext.Notifications
            .Select(n => n.Mensagem)
            .ToArray();

        var response = ApiResponse<object>.Fail(erros, HttpStatusCode.BadRequest);
        return BadRequest(response);
    }
}
