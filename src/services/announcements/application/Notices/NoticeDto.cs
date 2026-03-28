namespace Advent.Announcements.Application.Notices;

public sealed class NoticeDto
{
    public Guid? Id { get; set; }
    
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateOnly StartDate { get; init; }
    public required DateOnly? EndDate { get; init; }
}