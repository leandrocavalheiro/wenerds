using WeNerds.Models.Dto;

namespace WeNerds.Notification.Interfaces;

public interface INotificationService
{
    void BadRequest(string messageCode, string message, string field = "");
    void BadRequest(WeValidatorError validationError);
    void BadRequest(ICollection<WeValidatorError> validationError);        
    void Forbidden(string messageCode, string message);
    void NotFound(string messageCode, string message);
    void Unauthorized(string messageCode, string message);
    void InternalError(string messageCode, string message);
    IEnumerable<WeNotification> GetNotifications();
    bool HasNotifications();
    public bool HasNotificationsWithNotFoundStatus();
}
