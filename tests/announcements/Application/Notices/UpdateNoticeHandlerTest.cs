using Advent.Announcements.Application;
using Advent.Announcements.Application.Notices.Update;
using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Tests.Application.Notices;

public class UpdateNoticeHandlerTest
{
    [Fact]
    public async Task GivenValidRequestWhenHandleCalledThenNoticeIsUpdated()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();
        var unitOfWork = Mock.Of<IAnnouncementUnitOfWork>();

        var noticeId = Guid.NewGuid();

        var notice = new Notice(
            "Old Title",
            "Old Description",
            DateOnly.FromDateTime(DateTime.UtcNow),
            null,
            Guid.NewGuid()
        );

        Mock.Get(repository)
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken))
            .ReturnsAsync(notice);

        var handler = new UpdateNoticeHandler(repository, unitOfWork);

        var request = new UpdateNoticeRequest(
            noticeId,
            "New Title",
            "New Description",
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            null
        );

        // Act
        var response = await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(notice.Id, response.Id);
        Assert.Equal("New Title", response.Title);
        Assert.Equal("New Description", response.Description);

        Mock.Get(repository)
            .Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken), Times.Once);

        Mock.Get(unitOfWork)
            .Verify(uw => uw.SaveChangesAsync(TestContext.Current.CancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenInexistentNoticeIdWhenHandleCalledThenThrowsException()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();
        var unitOfWork = Mock.Of<IAnnouncementUnitOfWork>();

        Mock.Get(repository)
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken))
            .ReturnsAsync((Notice?)null);

        var handler = new UpdateNoticeHandler(repository, unitOfWork);

        var request = new UpdateNoticeRequest(
            Guid.NewGuid(),
            "Title",
            "Description",
            DateOnly.FromDateTime(DateTime.UtcNow),
            null
        );

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.HandleAsync(request, TestContext.Current.CancellationToken));

        Assert.StartsWith(Resource.NoticeNotFound, exception.Message);
    }
}