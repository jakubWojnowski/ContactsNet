using ContactsNet.Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsNet.Core.Middlewares;

internal static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        => services.AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<IExceptionResponseMapper, ExceptionToResponseMapper>()
            .AddSingleton<IExceptionCompositionRoot, ExceptionCompositionRoot>();
        
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ErrorHandlerMiddleware>();
}