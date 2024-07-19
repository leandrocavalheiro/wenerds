using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WeNerds.Models.Dto;
using WeNerds.Models.Responses;
using WeNerds.Services.Interfaces;


namespace WeNerds.Filters.Implementations;

public class WeResultHandlerFilter : IAsyncResultFilter
{
    private IWeNotificationService _weNotificationService;
    public WeResultHandlerFilter(IWeNotificationService weNotificationService)
    {
        _weNotificationService = weNotificationService;
    }
 
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_weNotificationService.HasNotifications())
        {
            // Ensure the result is an ObjectResult
            if (context.Result is ObjectResult objectResult)
            {
                // Set the appropriate status code based on the notifications
                if (_weNotificationService.HasNotificationsWithForbbidenStatus())
                    objectResult.StatusCode = StatusCodes.Status403Forbidden;
                else if (_weNotificationService.HasNotificationsWithUnauthorizedtatus())
                    objectResult.StatusCode = StatusCodes.Status401Unauthorized;
                else if (_weNotificationService.HasNotificationsWithNotFoundStatus())
                    objectResult.StatusCode = StatusCodes.Status404NotFound;
                else
                    objectResult.StatusCode = StatusCodes.Status400BadRequest;

                // Create and set the error response                
                objectResult.Value = new WeResponse<IEnumerable<WeNotification>>(false, _weNotificationService.GetNotifications());

                // Set the modified result back to the context
                context.Result = objectResult;
            }

            // Return early to prevent further processing
            await next();
            return;
        }

        // Execute the next delegate/middleware in the pipeline
        await next();
    }
}