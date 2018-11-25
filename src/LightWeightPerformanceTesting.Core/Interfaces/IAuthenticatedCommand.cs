using MediatR;

namespace LightWeightPerformanceTesting.Core.Interfaces
{
    public interface IAuthenticatedCommand<TResponse>: IAuthenticatedRequest, IRequest<TResponse>, ICommand<TResponse>
    {
    }
}
