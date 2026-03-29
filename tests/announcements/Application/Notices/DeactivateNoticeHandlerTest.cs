using Advent.Announcements.Application;
using Advent.Announcements.Application.Notices.Deactivate;
using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Tests.Application.Notices;

public class DeactivateNoticeHandlerTest
{
    [Fact]
    public async Task GivenExistingNoticeWhenHandleCalledThenNoticeIsDeactivated()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();
        var unitOfWork = Mock.Of<IAnnouncementUnitOfWork>();

        var noticeId = Guid.NewGuid();

        var notice = new Notice(
            "Título Teste",
            "Descrição Teste",
            DateOnly.FromDateTime(DateTime.UtcNow),
            null,
            Guid.NewGuid()
        );

        Mock.Get(repository)
            .Setup(repo => repo.GetByIdAsync(noticeId, TestContext.Current.CancellationToken))
            .ReturnsAsync(notice);

        var handler = new DeactivateNoticeHandler(repository, unitOfWork);
        var request = new DeactivateNoticeRequest(noticeId);

        // Act
        await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        // Assert
        Assert.True(notice.IsDeleted);
        Assert.NotNull(notice.DeletedAt);

        Mock.Get(repository)
            .Verify(repo => repo.GetByIdAsync(noticeId, TestContext.Current.CancellationToken), Times.Once);

        Mock.Get(unitOfWork)
            .Verify(uw => uw.SaveChangesAsync(TestContext.Current.CancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenInexistentNoticeWhenHandleCalledThenThrowsException()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();
        var unitOfWork = Mock.Of<IAnnouncementUnitOfWork>();

        var noticeId = Guid.NewGuid();

        Mock.Get(repository)
            .Setup(repo => repo.GetByIdAsync(noticeId, TestContext.Current.CancellationToken))
            .ReturnsAsync((Notice?)null);

        var handler = new DeactivateNoticeHandler(repository, unitOfWork);
        var request = new DeactivateNoticeRequest(noticeId);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.HandleAsync(request, TestContext.Current.CancellationToken));

        Assert.StartsWith(Resource.NoticeNotFound, exception.Message);

        Mock.Get(unitOfWork)
            .Verify(uw => uw.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}