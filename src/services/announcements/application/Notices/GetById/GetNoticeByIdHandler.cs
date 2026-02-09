using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.GetById;

public class GetNoticeByIdHandler(INoticeRepository repository) : IGetNoticeByIdHandler
{
    public Task<GetNoticeByIdResponse> HandleAsync(GetNoticeByIdRequest request, CancellationToken cancellationToken)
    {
        var notice = repository.GetById(request.Id);

        if (notice is null)
            throw new InvalidOperationException("Notice not found");

        return Task.FromResult(notice.ToResponse());
    }
}