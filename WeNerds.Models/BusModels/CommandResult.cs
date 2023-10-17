namespace WeNerds.Models.BusModels;

public class CommandResult<TResponse>
{
    public bool IsSuccess { get; set; }
    public TResponse Result { get; set; }
    
    public CommandResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public CommandResult(bool isSuccess, TResponse result)
    {
        IsSuccess = isSuccess;
        Result = result;
    }

    public void Deconstruct(out bool isSuccess, out TResponse result)
    {
        isSuccess = IsSuccess;
        result = Result;
    }
}
