using Advent.Announcements.Application.Notices;
using Advent.Announcements.Application.Notices.Activate;
using Advent.Announcements.Application.Notices.Create;
using Advent.Announcements.Application.Notices.Deactivate;
using Advent.Announcements.Application.Notices.GetActives;
using Advent.Announcements.Application.Notices.GetById;
using Advent.Announcements.Application.Notices.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Advent.Announcements.Application;

public static class AnnouncementsServices
{
    public static IServiceCollection AddAnnouncementsApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IActionHandler<UpdateNoticeRequest>, UpdateNoticeHandler>();
        services.AddScoped<IActionHandler<ActivateNoticeRequest>, ActivateNoticeHandler>();
        services.AddScoped<IFuncHandler<NoticeDto, NoticeDto>, CreateNoticeHandler>();
        services.AddScoped<IActionHandler<DeactivateNoticeRequest>, DeactivateNoticeHandler>();
        services.AddScoped<IFuncHandler<GetNoticeByIdRequest, NoticeDto>, GetNoticeByIdHandler>();
        services.AddScoped<IFuncHandler<GetActivesNoticeRequest, IEnumerable<GetActivesNoticeResponse>>, GetActivesNoticeHandler>();

        return services;
    }
}