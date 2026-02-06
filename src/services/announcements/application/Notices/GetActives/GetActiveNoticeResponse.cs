namespace Advent.Announcements.Application.Notices.GetActives;

public record GetActiveNoticeResponse(
    Guid Id,
    string Title,
    string Description,
    DateOnly StartDate,
    DateOnly? EndDate
);