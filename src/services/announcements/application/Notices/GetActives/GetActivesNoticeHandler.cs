using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.GetActives;

public class GetActivesNoticeHandler(INoticeRepository repository) : IGetActivesNoticeHandler
{
    public Task<IEnumerable<GetActivesNoticeResponse>> HandleAsync(
        GetActivesNoticeRequest request, CancellationToken cancellationToken)
    {
        var notice = repository.GetActives();
        var response = notice.Select(n => n.ToResponse());
        return Task.FromResult(response);
    }
}
