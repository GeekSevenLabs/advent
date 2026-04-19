using Advent.Announcements.Application.Notices.GetActives;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Tests.Application.Notices;

public class GetActivesNoticeHandlerTest
{
    [Fact]
    public async Task GivenExistingActiveNoticesWhenHandleCalledThenReturnsList()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();

        var fakeNotices = new List<Notice>
        {
            new Notice("Aviso 1", "Desc 1", DateOnly.FromDateTime(DateTime.UtcNow), null, Guid.NewGuid()),
            new Notice("Aviso 2", "Desc 2", DateOnly.FromDateTime(DateTime.UtcNow), null, Guid.NewGuid())
        };

        Mock.Get(repository)
            .Setup(repo => repo.GetActivesAsync(TestContext.Current.CancellationToken))
            .ReturnsAsync(fakeNotices);

        var handler = new GetActivesNoticeHandler(repository);
        var request = new GetActivesNoticeRequest();

        // Act
        var response = await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(response);

        var result = response.ToArray();

        Assert.Equal(2, result.Length);
        Assert.Contains(result, r => r.Title == "Aviso 1" && r.Description == "Desc 1");
        Assert.Contains(result, r => r.Title == "Aviso 2" && r.Description == "Desc 2");

        Mock.Get(repository)
            .Verify(repo => repo.GetActivesAsync(TestContext.Current.CancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenNoActiveNoticesWhenHandleCalledThenReturnsEmptyList()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();

        Mock.Get(repository)
            .Setup(repo => repo.GetActivesAsync(TestContext.Current.CancellationToken))
            .ReturnsAsync(new List<Notice>());

        var handler = new GetActivesNoticeHandler(repository);
        var request = new GetActivesNoticeRequest();

        // Act
        var response = await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Empty(response);

        Mock.Get(repository)
            .Verify(repo => repo.GetActivesAsync(TestContext.Current.CancellationToken), Times.Once);
    }
}