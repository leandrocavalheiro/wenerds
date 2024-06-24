namespace WeNerds.Models.Types;

public record struct WeSystemMessage
{
    public string[] Messages { get; set; }
    public WeSystemMessage(IEnumerable<string> errors)
        => Messages = errors.ToArray();
    public WeSystemMessage(string[] errors)
        => Messages = errors;
    public WeSystemMessage(string error)
        => Messages = [error];    
}
