using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.GetActives;

public class GetActivesNoticeHandler(INoticeRepository repository) : IFuncHandler<GetActivesNoticeRequest, IEnumerable<GetActivesNoticeResponse>>
{
    public async Task<IEnumerable<GetActivesNoticeResponse>> HandleAsync(GetActivesNoticeRequest request, CancellationToken cancellationToken)
    {
        var noticies = await repository.GetActivesAsync(cancellationToken);
        return noticies.ToResponse();
    }
}
