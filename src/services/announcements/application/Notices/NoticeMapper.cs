using Advent.Announcements.Application.Notices.Activate;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices;

internal static class NoticeMapper
{
    extension (Notice notice)
    {
        public NoticeDto ToResponse() => new()
        {
            Id = notice.Id,
            Title = notice.Title,
            Description = notice.Description,
            StartDate = notice.StartDate,
            EndDate = notice.EndDate,
        };
        
    }

    extension(NoticeDto notice)
    {
        public Notice ToEntity(Guid userId) => new(
            notice.Title,
            notice.Description,
            notice.StartDate,
            notice.EndDate,
            userId
        );
    }
}