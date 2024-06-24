using WeNerds.Models.Extensions;

namespace WeNerds.Models.Responses;

public class WeResponse<TTYpe>(bool Success = false, TTYpe Data = default)
{
    public bool IsSuccess()
        => Success;
    public override string ToString()
        => this.Serialize();
    
}
