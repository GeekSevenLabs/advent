namespace Advent.Announcements.Application.Notices.Deactivate;

public record DeactivateNoticeResponse(
    Guid Id,
    DateTimeOffset DeletedAt
);
