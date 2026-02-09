namespace Advent.Announcements.Application.Notices.Activate;

public record ActivateNoticeResponse(
    Guid Id,
    string Title,
    string description
);
