using Advent.Announcements.Domain.Notices;
using Advent.Announcements.Infrastructure.Configurations;

namespace Advent.Announcements.Infrastructure.Contexts;

internal class AnnouncementDbContext(DbContextOptions<AnnouncementDbContext> options) : DbContext(options)
{
    public required DbSet<Notice> Notices { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new NoticeConfiguration());
    }
}

