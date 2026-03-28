namespace Advent.Announcements.Application.Notices.Create;

public interface ICreateNoticeHandler
{
    Task<NoticeDto> HandleAsync(NoticeDto request, CancellationToken cancellationToken);
}