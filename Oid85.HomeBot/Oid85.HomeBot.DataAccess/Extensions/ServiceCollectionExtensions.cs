using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oid85.HomeBot.Common.Helpers;
using Oid85.HomeBot.Common.KnownConstants;
using Oid85.HomeBot.DataAccess.Interceptors;

namespace Oid85.HomeBot.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureFinMarketDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
                
        services.AddDbContext<HomeBotContext>((serviceProvider, options) =>
        {
            var updateInterceptor = serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>();
                
            options
                .UseNpgsql(ConvertHelper.Base64Decode(
                    configuration.GetValue<string>(KnownSettingsKeys.PostgresHomeBotConnectionString)!))
                .AddInterceptors(updateInterceptor);
        });
    }
    
    public static async Task ApplyMigrations(this IHost host)
    {
        var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        await using var scope = scopeFactory.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<HomeBotContext>();
        await context.Database.MigrateAsync();
    }
}