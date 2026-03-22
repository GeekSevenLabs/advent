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
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken))
            .ReturnsAsync(notice);

        var handler = new DeactivateNoticeHandler(repository, unitOfWork);
        var request = new DeactivateNoticeRequest(noticeId);

        // Act
        var response = await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(notice.Id, response.Id);
        Assert.NotEqual(default, response.DeletedAt);
        Assert.True(notice.IsDeleted);

        Mock.Get(repository)
            .Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken), Times.Once);

        Mock.Get(unitOfWork)
            .Verify(uw => uw.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
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

        var handler = new DeactivateNoticeHandler(repository, unitOfWork);
        var request = new DeactivateNoticeRequest(Guid.NewGuid());

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.HandleAsync(request, TestContext.Current.CancellationToken));
    }
}
