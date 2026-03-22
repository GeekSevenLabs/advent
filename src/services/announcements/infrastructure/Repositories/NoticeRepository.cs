using Advent.Announcements.Domain.Notices;
using Advent.Announcements.Infrastructure.Contexts;

namespace Advent.Announcements.Infrastructure.Repositories;

internal class NoticeRepository(AnnouncementDbContext db)
    : INoticeRepository
{
    public void Add(Notice notice, CancellationToken _)
    {
        db.Notices.Add(notice);
    }

    public async Task<IEnumerable<Notice>> GetActivesAsync(CancellationToken  cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        return await db.Notices
            .Where(n =>
            !n.IsDeleted &&
            n.StartDate <= today &&
            (n.EndDate == null || n.EndDate >= today))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Notice>> GetActivesByPeriodAsync(DateOnly start, DateOnly end, CancellationToken  cancellationToken)
    {
        return await db.Notices
            .Where(n =>
            !n.IsDeleted &&
            n.StartDate <= end &&
            (n.EndDate == null || n.EndDate >= start))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Notice?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await db.Notices.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }
}
