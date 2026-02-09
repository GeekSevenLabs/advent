namespace Advent.Announcements.Application.Notices.Activate;

public interface IActivateNoticeHandler
{
    Task<ActivateNoticeResponse> HandleAsync(ActivateNoticeRequest request, CancellationToken cancellationToken);
}
    
