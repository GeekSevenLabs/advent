using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Application.Notices.Create;

internal static class CreateNoticeMapper
{
    extension(Notice notice)
    {
        public CreateNoticeResponse ToResponse()
        {
            return new CreateNoticeResponse(
                notice.Id,
                notice.Title,
                notice.Description,
                notice.StartDate,
                notice.EndDate
            );
        }
    }

    extension(CreateNoticeRequest request)
    {
        public Notice ToEntity()
        {
            return new Notice(
                request.Title,
                request.Description,
                request.StartDate,
                request.EndDate,
                Guid.NewGuid() // TODO: Replace with actual user ID from context
            );
        }
        
    }

}