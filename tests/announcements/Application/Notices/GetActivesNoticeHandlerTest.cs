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
            .Setup(repo => repo.GetActives())
            .Returns(fakeNotices);

        var handler = new GetActivesNoticeHandler(repository);
        var request = new GetActivesNoticeRequest();

        // Act
        var response = await handler.HandlerAsync(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(2, response.Count());
        Assert.Contains(response, r => r.Title == "Aviso 1");
        Assert.Contains(response, r => r.Title == "Aviso 2");

        Mock.Get(repository)
            .Verify(repo => repo.GetActives(), Times.Once);
    }
}
