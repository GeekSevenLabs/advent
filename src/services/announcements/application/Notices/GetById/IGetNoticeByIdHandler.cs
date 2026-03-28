namespace Advent.Announcements.Application.Notices.GetById;

public interface IGetNoticeByIdHandler
{
    Task<NoticeDto> HandleAsync(GetNoticeByIdRequest request, CancellationToken cancellationToken);
}