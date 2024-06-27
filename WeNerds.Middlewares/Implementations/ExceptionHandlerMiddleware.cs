using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Mime;
using WeNerds.Models.Dto;


namespace WeNerds.Middlewares.Implementations;

public static class ExceptionHandlerMiddleware
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app, IHostEnvironment environment)
    {
        return app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature is null || environment.IsProduction())
                    await context.Response.WriteAsync(GetInternalError());
                        
                var logError = GetLogError(contextFeature);
                Console.WriteLine(logError);

                await context.Response.WriteAsync(GetInternalErrorDebugMode(contextFeature));

            });
        });
    }
    private static string GetInnerException(IExceptionHandlerFeature exception)
    {
        if (exception.Error.InnerException is not null)
            return exception.Error.InnerException.Message;

        return null;
    }

    private static string GetInternalError()    
        => (new WeNotification("", "InternalError", "InternalError", StatusCodes.Status500InternalServerError)).ToString();

    private static string GetInternalErrorDebugMode(IExceptionHandlerFeature exception)
        => (new WeNotification("", "InternalError", GetLogError(exception), StatusCodes.Status500InternalServerError)).ToString();

    private static string GetLogError(IExceptionHandlerFeature exceptionHandler)
        => $"Message: {exceptionHandler.Error.Message}|InnerException: {exceptionHandler.Error.InnerException?.Message}|StackTrace:{exceptionHandler.Error.StackTrace}";


}
