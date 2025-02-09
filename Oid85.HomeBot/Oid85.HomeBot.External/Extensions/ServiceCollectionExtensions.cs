using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oid85.HomeBot.Common.Helpers;
using Oid85.HomeBot.Common.KnownConstants;
using Oid85.HomeBot.External.ResourceStore;
using Oid85.HomeBot.External.Telegram;
using Telegram.Bot;

namespace Oid85.HomeBot.External.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureExternalServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<ITelegramService, TelegramService>();
        services.AddTransient<IResourceStoreService, ResourceStoreService>();
        
        var botClient = new TelegramBotClient(
            ConvertHelper.Base64Decode(
                configuration.GetValue<string>(KnownSettingsKeys.TelegramToken)!));
        
        services.AddSingleton(botClient);
    }
    
    public static async Task TelegramBotSubscribe(this IHost host)
    {
        var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        await using var scope = scopeFactory.CreateAsyncScope();
        var botClient = scope.ServiceProvider.GetRequiredService<TelegramBotClient>();
        var telegramService = scope.ServiceProvider.GetRequiredService<ITelegramService>();

        botClient.OnMessage += async (message, type) => 
            await telegramService.MessageHandleAsync(message, type);
    }
}