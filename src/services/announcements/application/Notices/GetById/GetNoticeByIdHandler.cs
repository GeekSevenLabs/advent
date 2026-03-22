using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.GetById;

public class GetNoticeByIdHandler(INoticeRepository repository) : IGetNoticeByIdHandler
{
    public async Task<GetNoticeByIdResponse> HandleAsync(GetNoticeByIdRequest request, CancellationToken cancellationToken)
    {
        var notice = await repository.GetByIdAsync(request.Id, cancellationToken);
        Throw.When.Null(notice, Resource.NoticeNotFound);
        return await Task.FromResult(notice.ToResponse());
    }
}