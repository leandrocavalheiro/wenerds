using WeNerds.Models.Dto;
using WeNerds.Services.Interfaces;

namespace WeNerds.Services.Implementation;

public class WeNotificationService : IWeNotificationService
{

    private const int BadRequestCode = 400;
    private const int ForbiddenCode = 403;
    private const int NotFoundCode = 404;
    private const int UnauthorizedCode = 401;
    private const int InternalErrorCode = 500;
    private readonly List<WeNotification> _notifications;
    public WeNotificationService()
    {
        _notifications = new List<WeNotification>();
    }
    public void BadRequest(string messageCode, string message, string field = "")
        => AddNotification(messageCode, message, BadRequestCode, field);
    public void BadRequest(WeValidatorError validationError)
        => AddNotification(validationError);
    public void BadRequest(ICollection<WeValidatorError> validationError)
        => AddNotification(validationError);
    public void Forbidden(string messageCode, string message)
        => AddNotification(messageCode, message, ForbiddenCode, "");
    public void NotFound(string messageCode, string message)
    => AddNotification(messageCode, message, NotFoundCode, "");
    public void Unauthorized(string messageCode, string message)
        => AddNotification(messageCode, message, UnauthorizedCode, "");
    public void InternalError(string messageCode, string message)
        => AddNotification(messageCode, message, InternalErrorCode, "");
    private void AddNotification(string messageCode, string message, int statusCode = 0, string field = "")
        => _notifications.Add(new WeNotification(field, messageCode, message, statusCode));
    private void AddNotification(WeValidatorError validationError, int statusCode = BadRequestCode)
        => AddNotification(validationError.ErrorCode, validationError.ErrorMessage, statusCode, validationError.PropertyName);    
    private void AddNotification(ICollection<WeValidatorError> validationError, int statusCode = BadRequestCode)
    {

        foreach (var error in validationError)
            AddNotification(error.ErrorCode, error.ErrorMessage, statusCode, error.PropertyName);

    }
    public IEnumerable<WeNotification> GetNotifications()
        => _notifications;
    public bool HasNotifications()
        => _notifications.Any();
    public bool HasNotificationsWithNotFoundStatus()
        => _notifications.Any(p => p.Status.Equals(NotFoundCode));
}
