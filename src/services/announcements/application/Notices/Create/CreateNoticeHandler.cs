using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Create;

public class CreateNoticeHandler(INoticeRepository repository, IAnnouncementUnitOfWork unitOfWork) : ICreateNoticeHandler
{
    public async Task<CreateNoticeResponse> HandleAsync(CreateNoticeRequest request, CancellationToken cancellationToken)
    {
        var notice = request.ToEntity();
        repository.Add(notice);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return notice.ToResponse();
    }
}