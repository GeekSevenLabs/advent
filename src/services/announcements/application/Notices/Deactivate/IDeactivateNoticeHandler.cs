using Advent.Announcements.Application.Notices.Deactivate;

namespace Advent.Announcements.Application.Notices.GetDeactivate;

public interface IDeactivateNoticeHandler
{
    Task<DeactivateNoticeResponse> HandleAsync(DeactivateNoticeRequest request, CancellationToken cancellationToken);
}
