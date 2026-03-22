using Advent.Announcements.Application.Notices.GetDeactivate;
using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Deactivate;

public class DeactivateNoticeHandler(INoticeRepository repository, IAnnouncementUnitOfWork unitOfWork) : IDeactivateNoticeHandler
{
    public async Task<DeactivateNoticeResponse> HandleAsync(DeactivateNoticeRequest request, CancellationToken cancellationToken)
    {
        var notice = repository.GetById(request.Id);
        if (notice is null)
            throw new InvalidOperationException("Notice not found");

        notice.Deactivate();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return notice.ToResponse();
    }
}
