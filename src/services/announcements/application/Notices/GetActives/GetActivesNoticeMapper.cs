using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.GetActives;

internal static class GetActivesNoticeMapper
{
    extension(Notice notice)
    {
        public GetActivesNoticeResponse ToResponse()
        {
            return new GetActivesNoticeResponse(
                notice.Id,
                notice.Title,
                notice.Description,
                notice.StartDate,
                notice.EndDate
            );
        }
    }
}
