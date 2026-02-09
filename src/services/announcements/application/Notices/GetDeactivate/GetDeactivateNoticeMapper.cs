using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.GetDeactivate;

internal static class GetDeactivateNoticeMapper
{
    extension(Notice notice)
    {
        public GetDeactivateNoticeResponse ToResponse()
        {
            return new GetDeactivateNoticeResponse(
                notice.Id,
                notice.DeletedAt!.Value
            );
        }
    }
}
