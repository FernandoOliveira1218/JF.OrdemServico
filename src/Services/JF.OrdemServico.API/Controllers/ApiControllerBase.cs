using AutoMapper;
using JF.OrdemServico.API.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JF.OrdemServico.Services.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly IMapper Mapper;

    protected ApiControllerBase(IMapper mapper)
    {
        Mapper = mapper;
    }

    protected IActionResult ResponseResult<T>(T? dto, string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
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
        var response = ApiResponse<T>.Created(dto, message ?? "Criado com sucesso");
        return Created(string.Empty, response);
    }

    protected IActionResult ResponseDeleted(string? message = null)
    {
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
}
