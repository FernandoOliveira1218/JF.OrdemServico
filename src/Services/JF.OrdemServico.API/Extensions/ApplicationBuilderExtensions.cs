namespace JF.OrdemServico.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAppConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
