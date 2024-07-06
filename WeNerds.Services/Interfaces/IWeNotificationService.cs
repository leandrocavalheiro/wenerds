using WeNerds.Models.Dto;

namespace WeNerds.Services.Interfaces;

public interface IWeNotificationService
{
    void BadRequest(string messageCode, string message, string field = "");
    void BadRequest(ICollection<WeValidatorError> validationError);        
    void BadRequest(WeValidatorError validationError);
    void Forbidden(string messageCode, string message);
    void NotFound(string messageCode, string message);
    void Unauthorized(string messageCode, string message);
    void InternalError(string messageCode, string message);
    IEnumerable<WeNotification> GetNotifications();
    bool HasNotifications();
    bool HasNotificationsWithNotFoundStatus();
    bool HasNotificationsWithForbbidenStatus();
    bool HasNotificationsWithUnauthorizedtatus();        
}
