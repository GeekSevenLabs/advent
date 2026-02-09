using Advent.Announcements.Application.Notices.GetDeactivate;
using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Tests.Application.Notices;

public class GetDeactivateNoticeHandlerTest
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
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns(notice);

        var handler = new GetDeactivateNoticeHandler(repository, unitOfWork);
        var request = new GetDeactivateNoticeRequest(noticeId);

        // Act
        var response = await handler.HandleAsync(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(notice.Id, response.Id);
        Assert.NotEqual(default(DateTimeOffset), response.DeletedAt);
        Assert.True(notice.IsDeleted);

        Mock.Get(repository)
            .Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);

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
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns((Notice?)null);

        var handler = new GetDeactivateNoticeHandler(repository, unitOfWork);
        var request = new GetDeactivateNoticeRequest(Guid.NewGuid());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.HandleAsync(request, CancellationToken.None));
    }
}
