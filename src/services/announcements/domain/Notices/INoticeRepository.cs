namespace Advent.Announcements.Domain.Notices;

public interface INoticeRepository : IRepository<Notice> 
{
    void Add(Notice notice);
    Notice? GetById(Guid id);
    IEnumerable<Notice> GetActive();
    IEnumerable<Notice> GetActiveByDate(DateTime date);
}
