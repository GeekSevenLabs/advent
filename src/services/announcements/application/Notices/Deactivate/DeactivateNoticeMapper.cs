using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Deactivate;

internal static class DeactivateNoticeMapper
{
    extension(Notice notice)
    {
        public DeactivateNoticeResponse ToResponse()
        {
            return new DeactivateNoticeResponse(
                notice.Id,
                notice.DeletedAt!.Value
            );
        }
    }
}
