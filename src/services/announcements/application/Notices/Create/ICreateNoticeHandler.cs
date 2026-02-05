namespace Advent.Announcements.Application.Notices.Create;

public interface ICreateNoticeHandler
{
    Task<CreateNoticeResponse> HandleAsync(CreateNoticeRequest request, CancellationToken cancellationToken);
}