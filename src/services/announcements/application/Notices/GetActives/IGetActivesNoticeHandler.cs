namespace Advent.Announcements.Application.Notices.GetActives;

public interface IGetActivesNoticeHandler
{
    Task<IEnumerable<GetActivesNoticeResponse>> HandleAsync(GetActivesNoticeRequest request, CancellationToken cancellationToken);
}
