namespace Advent;

public interface IFuncHandler<TResponse>
{
    Task<TResponse> HandleAsync(CancellationToken cancellationToken);
}

public interface IFuncHandler<in TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
