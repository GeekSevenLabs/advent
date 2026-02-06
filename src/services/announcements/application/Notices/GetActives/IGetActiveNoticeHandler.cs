namespace Advent.Announcements.Application.Notices.GetActives;

public interface IGetActiveNoticeHandler
{
    Task<IEnumerable<GetActiveNoticeResponse>> HandlerAsync(GetActiveNoticeRequest request, CancellationToken cancellationToken);
}
