using JF.OrdemServico.API.DTOs.Response;
using System.Net;
using System.Text.Json;
using FluentValidation;

namespace JF.OrdemServico.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro não tratado.");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        HttpStatusCode statusCode;
        string message;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                message = string.Join(" | ", validationException.Errors.Select(e => e.ErrorMessage));
                break;
            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = "Recurso não encontrado.";
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = "Ocorreu um erro interno no servidor.";
                break;
        }

        context.Response.StatusCode = (int)statusCode;

        var response = ApiResponse<object>.Fail(message, statusCode);

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
