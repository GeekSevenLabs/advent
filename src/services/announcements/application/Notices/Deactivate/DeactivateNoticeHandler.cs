using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Deactivate;

public class DeactivateNoticeHandler(INoticeRepository repository, IAnnouncementUnitOfWork unitOfWork) : IDeactivateNoticeHandler
{
    public async Task<DeactivateNoticeResponse> HandleAsync(DeactivateNoticeRequest request, CancellationToken cancellationToken)
    {
        var notice = await repository.GetByIdAsync(request.Id, cancellationToken);
        Throw.When.Null(notice, Resource.NoticeNotFound);

        notice.Deactivate();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return notice.ToResponse();
    }
}
