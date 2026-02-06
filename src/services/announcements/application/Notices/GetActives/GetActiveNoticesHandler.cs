using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.GetActives;

public class GetActiveNoticesHandler(INoticeRepository repository) : IGetActiveNoticeHandler
{
    public Task<IEnumerable<GetActiveNoticeResponse>> HandlerAsync(
        GetActiveNoticeRequest request, CancellationToken cancellationToken)
    {
        var notice = repository.GetActives();
        var response = notice.Select(n => n.ToResponse());
        return Task.FromResult(response);
    }
}
