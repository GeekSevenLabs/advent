using Advent.Announcements.Application;
using Advent.Announcements.Application.Notices.GetById;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Tests.Application.Notices;

public class GetNoticeByIdHandlerTest
{
    [Fact]
    public async Task GivenExistingNoticeIdWhenHandleCalledThenReturnsNoticeResponse()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();

        var noticeId = Guid.NewGuid();
        var notice = new Notice(
            "Título do Aviso",
            "Descrição do Aviso",
            DateOnly.FromDateTime(DateTime.UtcNow),
            null,
            Guid.NewGuid()
        );

        Mock.Get(repository)
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken))
            .ReturnsAsync(notice);

        var handler = new GetNoticeByIdHandler(repository);
        var request = new GetNoticeByIdRequest(noticeId);

        // Act
        var response = await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(notice.Id, response.Id);
        Assert.Equal("Título do Aviso", response.Title);

        Mock.Get(repository)
            .Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenInexistentNoticeIdWhenHandleCalledThenThrowsException()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();

        Mock.Get(repository)
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), TestContext.Current.CancellationToken))
            .ReturnsAsync((Notice?)null);

        var handler = new GetNoticeByIdHandler(repository);
        var request = new GetNoticeByIdRequest(Guid.NewGuid());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.HandleAsync(request, TestContext.Current.CancellationToken));

        Assert.StartsWith(Resource.NoticeNotFound, exception.Message);
    }
}
