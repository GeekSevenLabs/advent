namespace Advent.Announcements.Application.Notices.Create;

public record CreateNoticeResponse(Guid Id, string Title, string Description, DateOnly StartDate, DateOnly? EndDate);