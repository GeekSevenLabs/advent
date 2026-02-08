namespace Advent.Announcements.Application.Notices.GetActives;

public interface IGetActivesNoticeHandler
{
    Task<IEnumerable<GetActivesNoticeResponse>> HandlerAsync(GetActivesNoticeRequest request, CancellationToken cancellationToken);
}
