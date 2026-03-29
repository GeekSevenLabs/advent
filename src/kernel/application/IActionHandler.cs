namespace Advent;

public interface IActionHandler
{
    Task HandleAsync(CancellationToken cancellationToken);
}

public interface IActionHandler<in TRequest>
{
    Task HandleAsync(TRequest request, CancellationToken cancellationToken);
}
