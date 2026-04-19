using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Update;

public class UpdateNoticeHandler(INoticeRepository noticeRepository, IAnnouncementUnitOfWork unitOfWork) : IActionHandler<UpdateNoticeRequest>
{
    public async Task HandleAsync(UpdateNoticeRequest request, CancellationToken cancellationToken)
    {
        var notice = await noticeRepository.GetByIdAsync(request.Id, cancellationToken);
        Throw.When.Null(notice, Resource.NoticeNotFound);

        notice.Update(request.Title, request.Description, request.StartDate, request.EndDate);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
