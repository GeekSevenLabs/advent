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
        var request = new CreateNoticeRequest(
            "Test Title", 
            "Test Content",
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            null
        );

        // Act
        var response = await handler.HandleAsync(request, CancellationToken.None);
        
        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal("Test Title", response.Title);
        
        Mock.Get(repository)
            .Verify(repo => repo.Add(It.IsAny<Notice>()), Times.Once);
        
        Mock.Get(unitOfWork)
            .Verify(uw => uw.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

}