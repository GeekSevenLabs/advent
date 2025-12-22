using Advent.Announcements.Domain.Notices;
using Advent.Announcements.Infrastructure.Contexts;

namespace Advent.Announcements.Infrastructure.Repositories;

internal class NoticeRepository(AnnouncementDbContext db)
    : INoticeRepository
{
    public void Add(Notice notice)
    {
        db.Notices.Add(notice);
    }

    public IEnumerable<Notice> GetActive()
    {
        var now = DateTime.UtcNow;

        return db.Notices
            .Where(n =>
            n.IsActive &&
            n.StartDate <= now &&
            (n.EndDate == null || n.EndDate >= now))
            .AsNoTracking()
            .ToList();
    }

    public IEnumerable<Notice> GetActiveByDate(DateTime date)
    {
        return db.Notices
            .Where(n =>
            n.IsActive &&
            n.StartDate <= date &&
            (n.EndDate == null || n.EndDate >= date))
            .AsNoTracking()
            .ToList();
    }

    public Notice? GetById(Guid id)
    {
        return db.Notices.FirstOrDefault(n => n.Id == id);
    }
}
