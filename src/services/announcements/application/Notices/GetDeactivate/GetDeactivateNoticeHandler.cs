using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.GetDeactivate;

public class GetDeactivateNoticeHandler(INoticeRepository repository, IAnnouncementUnitOfWork unitOfWork) : IGetDeactivateNoticeHandler
{
    public async Task<GetDeactivateNoticeResponse> HandleAsync(GetDeactivateNoticeRequest request, CancellationToken cancellationToken)
    {
        var notice = repository.GetById(request.Id);
        if (notice is null)
            throw new InvalidOperationException("Notice not found");

        notice.Deactivate();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return notice.ToResponse();
    }
}
