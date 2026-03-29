using Advent.Announcements.Application.Notices;
using Advent.Announcements.Application.Notices.Activate;
using Advent.Announcements.Application.Notices.Deactivate;
using Advent.Announcements.Application.Notices.GetActives;
using Advent.Announcements.Application.Notices.GetById;
using Advent.Announcements.Application.Notices.Update;

namespace Advent.Announcements.Api.Endpoints;

public static class NoticeEndpoints
{
    public static void MapNoticeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("api/notices").AllowAnonymous();

        group.MapPost("", Create);
        group.MapPut("", Update);
        group.MapPost("activate", Activate);
        group.MapPost("deactivate", Deactivate);
        group.MapGet("{id:guid}", GetById);
        group.MapGet("actives", GetActives);
    }

    private static async Task<IResult> Create(IFuncHandler<NoticeDto, NoticeDto> handler, NoticeDto request, CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(request, cancellationToken);
        return Results.Ok(response);
    }

    private static async Task<IResult> Update(IActionHandler<UpdateNoticeRequest> handler, UpdateNoticeRequest request, CancellationToken cancellationToken)
    {
        await handler.HandleAsync(request, cancellationToken);
        return Results.Ok();
    }
    private static async Task<IResult> Activate(IActionHandler<ActivateNoticeRequest> handler, ActivateNoticeRequest request, CancellationToken cancellationToken)
    {
        await handler.HandleAsync(request, cancellationToken);
        return Results.Ok();
    }

    private static async Task<IResult> Deactivate(IActionHandler<DeactivateNoticeRequest> handler, DeactivateNoticeRequest request, CancellationToken cancellationToken)
    {
        await handler.HandleAsync(request, cancellationToken);
        return Results.Ok();
    }

    private static async Task<IResult> GetById(IFuncHandler<GetNoticeByIdRequest, NoticeDto> handler, Guid id, CancellationToken cancellationToken)
    {
        var request = new GetNoticeByIdRequest(id);
        var response = await handler.HandleAsync(request, cancellationToken);
        return Results.Ok(response);
    }

    private static async Task<IResult> GetActives(IFuncHandler<GetActivesNoticeRequest, IEnumerable<GetActivesNoticeResponse>> handler, CancellationToken cancellationToken)
    {
        var request = new GetActivesNoticeRequest();
        var response = await handler.HandleAsync(request, cancellationToken);
        return Results.Ok(response);
    }
}