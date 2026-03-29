using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Advent.Announcements.Application;

public static class AnnouncementsServices
{
    public static IServiceCollection AddAnnouncementsServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Handlers
        // services.AddScoped<INoticeRepository, NoticeRepository>();
        //
        return services;
    }
}