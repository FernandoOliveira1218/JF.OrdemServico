using JF.OrdemServico.API.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace JF.OrdemServico.API.Filters;

public class ModelValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(e => e.Value?.Errors.Count > 0)
                .SelectMany(kvp => kvp.Value!.Errors.Select(err => err.ErrorMessage))
                .ToArray();

            var response = ApiResponse<object>.Fail(string.Join(" | ", errors), HttpStatusCode.BadRequest);
            context.Result = new BadRequestObjectResult(response);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // nada aqui
    }
}