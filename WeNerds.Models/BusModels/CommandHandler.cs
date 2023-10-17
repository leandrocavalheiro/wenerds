using WeNerds.Models.Interfaces;

namespace WeNerds.Models.BusModels;

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : Command
{
    public abstract Task<bool> Handle(TCommand request, CancellationToken cancellationToken);
}

public abstract class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse> where TCommand : Command<TResponse>
{
    public abstract Task<CommandResult<TResponse>> Handle(TCommand request, CancellationToken cancellationToken);
}
