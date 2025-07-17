using FluentValidation;
using JF.OrdemServico.API.DTOs.Response;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                message = "Erro(s) de validação encontrados.";
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

        var response = ApiResponse<object>.Fail(GetErrorsFromException(exception), statusCode, message);

        var json = JsonSerializer.Serialize(response, options: new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });

        return context.Response.WriteAsync(json);
    }

    private static IEnumerable<string> GetErrorsFromException(Exception ex)
    {
        if (ex is ValidationException validationException)
        {
            // Pega todas as mensagens de erro do FluentValidation
            return validationException.Errors.Select(e => e.ErrorMessage);
        }
        else if (ex is AggregateException aggregateException)
        {
            // Caso seja uma AggregateException, extrai mensagens das inner exceptions
            return aggregateException.InnerExceptions.SelectMany(inner => GetErrorsFromException(inner));
        }
        else if (ex.InnerException != null)
        {
            // Se tiver InnerException, extrai dela recursivamente
            return GetErrorsFromException(ex.InnerException);
        }
        else
        {
            // Caso geral: retorna a mensagem da exceção atual
            return [ex.Message];
        }
    }
}
