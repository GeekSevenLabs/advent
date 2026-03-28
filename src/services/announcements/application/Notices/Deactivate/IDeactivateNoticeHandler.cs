namespace Advent.Announcements.Application.Notices.Deactivate;

public interface IDeactivateNoticeHandler
{
    Task HandleAsync(DeactivateNoticeRequest request, CancellationToken cancellationToken);
}
