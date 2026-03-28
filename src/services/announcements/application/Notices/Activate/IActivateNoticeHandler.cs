namespace Advent.Announcements.Application.Notices.Activate;

public interface IActivateNoticeHandler
{
    Task HandleAsync(ActivateNoticeRequest request, CancellationToken cancellationToken);
}
