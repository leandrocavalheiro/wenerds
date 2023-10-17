using MediatR;
using WeNerds.Models.BusModels;

namespace WeNerds.Models.Interfaces;

public interface ICommand : IRequest<bool>
{
}

public interface ICommand<TResponse> : IRequest<CommandResult<TResponse>>
{
}
