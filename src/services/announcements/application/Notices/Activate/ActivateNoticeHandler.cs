using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Activate;

public class ActivateNoticeHandler(INoticeRepository repository, IAnnouncementUnitOfWork unitOfWork) : IActivateNoticeHandler
{
    public async Task<ActivateNoticeResponse> HandleAsync(ActivateNoticeRequest request, CancellationToken cancellationToken)
    {
        var notice = repository.GetById(request.Id);
        if (notice is null)
            throw new InvalidOperationException("Notice not found");

        notice.Activate();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return notice.ToResponse();
    }
}

