namespace Advent.Announcements.Application.Notices.Update;

public record UpdateNoticeResponse(
    Guid Id,
    string Title,
    string Description
);
