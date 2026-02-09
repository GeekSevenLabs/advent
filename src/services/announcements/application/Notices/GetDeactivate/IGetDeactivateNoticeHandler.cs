namespace Advent.Announcements.Application.Notices.GetDeactivate;

public interface IGetDeactivateNoticeHandler
{
    Task<GetDeactivateNoticeResponse> HandleAsync(GetDeactivateNoticeRequest request, CancellationToken cancellationToken);
}
