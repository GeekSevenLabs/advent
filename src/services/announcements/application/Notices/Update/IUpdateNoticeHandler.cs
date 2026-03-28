namespace Advent.Announcements.Application.Notices.Update;

public interface IUpdateNoticeHandler
{
    Task HandleAsync(NoticeDto request, CancellationToken cancellationToken);
}
