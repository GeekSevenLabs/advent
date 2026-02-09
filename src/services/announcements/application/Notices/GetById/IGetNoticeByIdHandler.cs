namespace Advent.Announcements.Application.Notices.GetById;

public interface IGetNoticeByIdHandler
{
    Task<GetNoticeByIdResponse> HandleAsync(GetNoticeByIdRequest request, CancellationToken cancellationToken);
}