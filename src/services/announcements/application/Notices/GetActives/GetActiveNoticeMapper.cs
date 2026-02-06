using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.GetActives;

internal static class GetActiveNoticeMapper
{
    extension(Notice notice)
    {
        public GetActiveNoticeResponse ToResponse()
        {
            return new GetActiveNoticeResponse(
                notice.Id,
                notice.Title,
                notice.Description,
                notice.StartDate,
                notice.EndDate
            );
        }
    }
}
