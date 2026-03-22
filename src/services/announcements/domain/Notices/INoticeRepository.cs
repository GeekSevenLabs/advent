namespace Advent.Announcements.Domain.Notices;

public interface INoticeRepository : IRepository<Notice> 
{
    void Add(Notice notice, CancellationToken cancellationToken);
    Task<Notice?> GetByIdAsync(Guid id,  CancellationToken cancellationToken);
    Task<IEnumerable<Notice>> GetActivesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Notice>> GetActivesByPeriodAsync(DateOnly start, DateOnly end, CancellationToken cancellationToken);
}
