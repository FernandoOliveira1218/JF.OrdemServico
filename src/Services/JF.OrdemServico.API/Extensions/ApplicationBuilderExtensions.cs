using JF.OrdemServico.API.Middleware;

namespace JF.OrdemServico.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAppConfiguration(this IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JF.OrdemServico API v1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        return app;
    }
}
