using Advent.Announcements.Application.Notices.Activate;

namespace Advent.Announcements.Api.Endpoints;

public static class NoticeEndpoints
{
    public static void MapNoticeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("api/notices").AllowAnonymous();

        group.MapPost("activate", Activate);
    }

    private static async Task<IResult> Activate(IActionHandler<ActivateNoticeRequest> handler, ActivateNoticeRequest request, CancellationToken cancellationToken)
    {
        await handler.HandleAsync(request, cancellationToken);
        return Results.Ok();
    }

}