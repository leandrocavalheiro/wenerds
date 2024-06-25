namespace WeNerds.Models.Requests;

public class WeGetByIdRequest(Guid id) : WeRequest
{
    public Guid Id { get; } = id;
}
