using System.Net;

namespace JF.OrdemServico.API.DTOs.Response;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? Errors { get; set; }
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static ApiResponse<T> Ok(T? data, string? message = null)
    {
        return new()
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = HttpStatusCode.OK
        };
    }

    public static ApiResponse<T> Created(T data, string? message = null)
    {
        return new()
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = HttpStatusCode.Created
        };
    }

    public static ApiResponse<T> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new()
        {
            Success = false,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static ApiResponse<T> Fail(IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest, string? message = null)
    {
        return new()
        {
            Success = false,
            Errors = errors,
            Message = message ?? "Ocorreu um erro.",
            StatusCode = statusCode
        };
    }

    public static ApiResponse<T> NotFound(string message = "Recurso não encontrado")
    {
        return new()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.NotFound
        };
    }
}