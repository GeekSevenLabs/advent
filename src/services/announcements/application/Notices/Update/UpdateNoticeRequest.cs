namespace Advent.Announcements.Application.Notices.Update;

public record UpdateNoticeRequest(
    Guid Id,
    string Title,
    string Description,
    DateOnly StartDate,
    DateOnly? EndDate
);