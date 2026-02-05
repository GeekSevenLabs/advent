namespace Advent.Announcements.Application.Notices.Create;

public record CreateNoticeRequest(string Title, string Description, DateOnly StartDate, DateOnly? EndDate);