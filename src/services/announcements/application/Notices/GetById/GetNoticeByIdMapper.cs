using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.GetById;

internal static class GetNoticeByIdMapper
{
    extension(Notice notice)
    {
        public GetNoticeByIdResponse ToResponse()
        {
            return new GetNoticeByIdResponse(
                notice.Id,
                notice.Title,
                notice.Description,
                notice.StartDate,
                notice.EndDate
            );
        }
    }
}