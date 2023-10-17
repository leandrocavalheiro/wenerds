using WeNerds.Models.Interfaces;

namespace WeNerds.Models.BusModels;

public abstract class Command : ICommand
{
}

public abstract class Command<TResponse> : ICommand<TResponse>
{
}
