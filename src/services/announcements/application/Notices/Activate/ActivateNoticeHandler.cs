using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Activate;

public class ActivateNoticeHandler(INoticeRepository repository, IAnnouncementUnitOfWork unitOfWork) : IActionHandler<ActivateNoticeRequest>
{
    public async Task HandleAsync(ActivateNoticeRequest request, CancellationToken cancellationToken)
    {
        var notice = await repository.GetByIdAsync(request.Id, cancellationToken);
        Throw.When.Null(notice, Resource.NoticeNotFound);
        
        notice.Activate();

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

}

