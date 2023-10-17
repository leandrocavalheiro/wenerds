using MediatR;

namespace WeNerds.Models.Interfaces;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}
