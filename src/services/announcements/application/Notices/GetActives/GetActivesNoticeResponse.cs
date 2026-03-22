namespace Advent.Announcements.Application.Notices.GetActives;

public record GetActivesNoticeResponse(
    Guid Id,
    string Title,
    string Description,
    DateOnly StartDate,
    DateOnly? EndDate
);