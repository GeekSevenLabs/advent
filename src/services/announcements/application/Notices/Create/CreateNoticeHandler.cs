using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Create;

public class CreateNoticeHandler(INoticeRepository repository, IAnnouncementUnitOfWork unitOfWork) : IFuncHandler<NoticeDto, NoticeDto>
{
    public async Task<NoticeDto> HandleAsync(NoticeDto request, CancellationToken cancellationToken)
    {
        // TODO: alterar para o pegar do user context
        var userId = Guid.CreateVersion7();
        var notice = request.ToEntity(userId);
        repository.Add(notice, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return notice.ToResponse();
    }
}