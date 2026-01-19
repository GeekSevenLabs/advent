using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Infrastructure.Configurations;

internal class NoticeConfiguration : IEntityTypeConfiguration<Notice>
{
    public void Configure(EntityTypeBuilder<Notice> builder)
    {
        builder.HasKey(notice => notice.Id);

        builder
            .Property(notice => notice.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(notice => notice.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder
            .Property(notice => notice.StartDate)
            .IsRequired();

        builder
            .Property(notice => notice.EndDate)
            .IsRequired(false);

        builder
            .Property(notice => notice.IsDeleted)
            .IsRequired();

        builder
            .Property(notice => notice.CreatedAt)
            .IsRequired();

        builder
            .Property(notice => notice.CreatedByUserId)
            .IsRequired();

        builder.HasIndex(notice => notice.IsDeleted);
        builder.HasIndex(notice => notice.StartDate);
    }
}
