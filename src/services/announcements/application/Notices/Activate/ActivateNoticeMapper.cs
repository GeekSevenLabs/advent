using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Activate;

public static class ActivateNoticeMapper
{
    extension (Notice notice)
    {
        public ActivateNoticeResponse ToResponse()
        {
            return new (
                notice.Id,
                notice.Title,
                notice.Description
            );
        }
    }
}
