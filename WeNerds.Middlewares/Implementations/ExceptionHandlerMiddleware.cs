using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using WeNerds.Commons;
using WeNerds.Models.Dto;
using WeNerds.Models.Responses;
using WeNerds.Models.Types;


namespace WeNerds.Middlewares.Implementations;

public static class ExceptionHandlerMiddleware
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app, IConfiguration configuration, IHostEnvironment environment, ILogger logger)
    {
        var showLog = configuration[WeConstants.EV_NAME_EXCEPTION_HANDLER_SHOW_LOG_PRODUCTION] == "true";        
        return app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var errorId = Guid.NewGuid();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature is null || environment.IsProduction())
                {
                    if(showLog)
                        LogError(logger, errorId, contextFeature);
                    
                    await context.Response.WriteAsync(GetInternalError(errorId));
                    return;
                }
                        
                var logError = GetLogError(contextFeature);
                LogError(logger, errorId, contextFeature);
                await context.Response.WriteAsync(GetInternalErrorDebugMode(contextFeature));
            });
        });
    }
    private static void LogError(ILogger logger, Guid errorId, IExceptionHandlerFeature exception)
        =>  logger.LogError(exception.Error, $"ErrorId: {errorId}");
    
    private static string GetInnerException(IExceptionHandlerFeature exception)
    {
        if (exception.Error.InnerException is not null)
            return exception.Error.InnerException.Message;

        return null;
    }

    private static string GetInternalError(Guid errorId)
    {
        var result = new WeResponse<WeNotification>(false, new WeNotification("", "InternalError", $"InternalError: {errorId}", StatusCodes.Status500InternalServerError));
        return result.ToString();        
    }

    private static string GetInternalErrorDebugMode(IExceptionHandlerFeature exception)
    {
        var result = new WeResponse<WeNotification>(false, new WeNotification("", "InternalError", GetLogError(exception), StatusCodes.Status500InternalServerError));        
        return result.ToString();
    }

    private static string GetLogError(IExceptionHandlerFeature exceptionHandler)
        => $"Message: {exceptionHandler.Error.Message}|InnerException: {exceptionHandler.Error.InnerException?.Message}|StackTrace:{exceptionHandler.Error.StackTrace}";


}
