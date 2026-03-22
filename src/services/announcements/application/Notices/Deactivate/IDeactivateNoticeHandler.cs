namespace Advent.Announcements.Application.Notices.Deactivate;

public interface IDeactivateNoticeHandler
{
    Task<DeactivateNoticeResponse> HandleAsync(DeactivateNoticeRequest request, CancellationToken cancellationToken);
}
