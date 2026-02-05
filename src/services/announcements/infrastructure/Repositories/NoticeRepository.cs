using Advent.Announcements.Domain.Notices;
using Advent.Announcements.Infrastructure.Contexts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Advent.Announcements.Infrastructure.Repositories;

internal class NoticeRepository(AnnouncementDbContext db)
    : INoticeRepository
{
    public void Add(Notice notice)
    {
        db.Notices.Add(notice);
    }

    public IEnumerable<Notice> GetActives()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        return db.Notices
            .Where(n =>
            !n.IsDeleted &&
            n.StartDate <= today &&
            (n.EndDate == null || n.EndDate >= today))
            .AsNoTracking()
            .ToList();
    }

    public IEnumerable<Notice> GetActivesByPeriod(DateOnly start, DateOnly end)
    {
        return db.Notices
            .Where(n =>
            !n.IsDeleted &&
            n.StartDate <= end &&
            (n.EndDate == null || n.EndDate >= start))
            .AsNoTracking()
            .ToList();
    }

    public Notice? GetById(Guid id)
    {
        return db.Notices.FirstOrDefault(n => n.Id == id);
    }
}
