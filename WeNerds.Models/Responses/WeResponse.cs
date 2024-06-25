using WeNerds.Models.Extensions;

namespace WeNerds.Models.Responses;

public class WeResponse<TTYpe>(bool success = false, TTYpe data = default)
{
    public bool Success { get; set; } = success;
    public TTYpe Data { get; set;} = data;
    public bool IsSuccess()
        => Success;
    public override string ToString()
        => this.Serialize();
    
}
