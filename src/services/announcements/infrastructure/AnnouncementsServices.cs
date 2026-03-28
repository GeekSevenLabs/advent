using Advent.Announcements.Domain;
using Advent.Announcements.Domain.Notices;
using Advent.Announcements.Infrastructure.Contexts;
using Advent.Announcements.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Advent.Announcements.Infrastructure;

public static class AnnouncementsServices
{
    private const string DbConnectionStringName = "DefaultConnection";
    
    public static IServiceCollection AddAnnouncementsServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Db
        services.AddDbContext<AnnouncementDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString(DbConnectionStringName))
        );
        
        services.AddScoped<IAnnouncementUnitOfWork>(sp => sp.GetRequiredService<AnnouncementDbContext>());
        
        // Repositories
        services.AddScoped<INoticeRepository, NoticeRepository>();
        
        return services;
    }
}