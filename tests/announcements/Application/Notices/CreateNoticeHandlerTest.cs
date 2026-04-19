using Advent.Announcements.Application.Notices;
using Advent.Announcements.Application.Notices.Create;
using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Tests.Application.Notices;

public class CreateNoticeHandlerTest
{
    [Fact]
    public async Task GivenValidRequestWhenHandleCalledThenNoticeIsCreated()
    {
        // Arrange
        var repository = Mock.Of<INoticeRepository>();
        var unitOfWork = Mock.Of<IAnnouncementUnitOfWork>();

        var handler = new CreateNoticeHandler(repository, unitOfWork);

        var request = new NoticeDto
        {
            Title = "Test Title",
            Description = "Test Content",
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            EndDate = null
        };

        // Act
        var response = await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal("Test Title", response.Title);
        Assert.Equal("Test Content", response.Description);

        Mock.Get(repository)
            .Verify(repo => repo.Add(
                It.Is<Notice>(n =>
                    n.Title == "Test Title" &&
                    n.Description == "Test Content"),
                TestContext.Current.CancellationToken),
                Times.Once);

        Mock.Get(unitOfWork)
            .Verify(uw => uw.SaveChangesAsync(TestContext.Current.CancellationToken), Times.Once);
    }
}