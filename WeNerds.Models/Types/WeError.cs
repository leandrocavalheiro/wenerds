using WeNerds.Models.Dto;

namespace WeNerds.Models.Types;

public struct WeError
{
    public WeNotification[] Errors { get; set; }

    public WeError(IEnumerable<WeNotification> errors)
        => Errors = errors.ToArray();
    public WeError(WeNotification[] errors)
        => Errors = errors;
    public WeError(WeNotification error)
        => Errors = [error];
    
}
