namespace Advent.Announcements.Application.Notices.GetById;

public record GetNoticeByIdResponse(
    Guid Id,
    string Title,
    string Description,
    DateOnly StartDate,
    DateOnly? EndDate
);