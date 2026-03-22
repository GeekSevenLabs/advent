using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Update;

internal static class UpdateNoticeMapper
{
    extension (Notice notice)
    {
        public UpdateNoticeResponse ToResponse()
        {
            return new(
                notice.Id,
                notice.Title,
                notice.Description
            );
        }
    }
}
