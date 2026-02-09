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

        Mock.Get(repository)
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns(notice);

        var handler = new ActivateNoticeHandler(repository, unitOfWork);
        var request = new ActivateNoticeRequest(notice.Id);

        // Act
        var response = await handler.HandleAsync(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(notice.IsDeleted);
        Assert.Null(notice.DeletedAt);

        Mock.Get(unitOfWork)
            .Verify(uw => uw.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GivenExpiredNoticeWhenHandleCalledThenThrowsException()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();
        var unitOfWork = Mock.Of<IAnnouncementUnitOfWork>();

        var expiredDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5));
        var notice = new Notice(
            "Título", "Descrição",
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10)),
            expiredDate,
            Guid.NewGuid()
        );

        Mock.Get(repository)
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns(notice);

        var handler = new ActivateNoticeHandler(repository, unitOfWork);
        var request = new ActivateNoticeRequest(notice.Id);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.HandleAsync(request, CancellationToken.None));

        Assert.Equal("Não é possível ativar um aviso expirado.", exception.Message);
    }

    [Fact]
    public async Task GivenInexistentNoticeWhenHandleCalledThenThrowsNotFoundException()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();
        var unitOfWork = Mock.Of<IAnnouncementUnitOfWork>();

        Mock.Get(repository)
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns((Notice?)null);

        var handler = new ActivateNoticeHandler(repository, unitOfWork);
        var request = new ActivateNoticeRequest(Guid.NewGuid());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.HandleAsync(request, CancellationToken.None));
    }
}

