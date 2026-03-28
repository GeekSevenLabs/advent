namespace Advent.Announcements.Application.Notices.Update;

public interface IUpdateNoticeHandler
{
    Task<UpdateNoticeResponse> HandleAsync(UpdateNoticeRequest request, CancellationToken cancellationToken);
}
