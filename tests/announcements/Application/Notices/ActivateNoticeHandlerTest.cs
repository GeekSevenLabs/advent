using Advent.Announcements.Application;
using Advent.Announcements.Application.Notices.Activate;
using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Tests.Application.Notices;

public class ActivateNoticeHandlerTest
{
    [Fact]
    public async Task GivenValidNoticeWhenHandleCalledThenNoticeIsActivated()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();
        var unitOfWork = Mock.Of<IAnnouncementUnitOfWork>();

        var notice = new Notice(
            "Título", "Descrição",
            DateOnly.FromDateTime(DateTime.UtcNow),
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
            Guid.NewGuid()
        );

        notice.Deactivate();

        Mock.Get(repository)
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken))
            .ReturnsAsync(notice);

        var handler = new ActivateNoticeHandler(repository, unitOfWork);
        var request = new ActivateNoticeRequest(notice.Id);

        // Act
        await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        // Assert
        Assert.False(notice.IsDeleted);
        Assert.Null(notice.DeletedAt);

        Mock.Get(unitOfWork)
            .Verify(uw => uw.SaveChangesAsync(TestContext.Current.CancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenExpiredNoticeWhenHandleCalledThenThrowsException()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();
        var unitOfWork = Mock.Of<IAnnouncementUnitOfWork>();

        var notice = new Notice(
            "Título", "Descrição",
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10)),
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5)),
            Guid.NewGuid()
        );

        Mock.Get(repository)
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken))
            .ReturnsAsync(notice);

        var handler = new ActivateNoticeHandler(repository, unitOfWork);
        var request = new ActivateNoticeRequest(notice.Id);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.HandleAsync(request, TestContext.Current.CancellationToken));

        Assert.Equal("Não é possível ativar um aviso expirado.", exception.Message);

        Mock.Get(unitOfWork)
            .Verify(uw => uw.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GivenInexistentNoticeWhenHandleCalledThenThrowsException()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();
        var unitOfWork = Mock.Of<IAnnouncementUnitOfWork>();

        Mock.Get(repository)
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken))
            .ReturnsAsync((Notice?)null);

        var handler = new ActivateNoticeHandler(repository, unitOfWork);
        var request = new ActivateNoticeRequest(Guid.NewGuid());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.HandleAsync(request, TestContext.Current.CancellationToken));

        Assert.StartsWith(Resource.NoticeNotFound, exception.Message);

        Mock.Get(unitOfWork)
            .Verify(uw => uw.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}