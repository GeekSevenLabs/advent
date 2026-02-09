namespace Advent.Announcements.Application.Notices.GetDeactivate;

public record GetDeactivateNoticeResponse(
    Guid Id,
    DateTimeOffset DeletedAt
);
