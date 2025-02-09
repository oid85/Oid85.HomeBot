using Oid85.HomeBot.Application.Interfaces.Services;
using Oid85.HomeBot.External.Telegram;

namespace Oid85.HomeBot.Application.Services;

/// <inheritdoc />
public class SendService(
    ITelegramService telegramService)
    : ISendService
{
    /// <inheritdoc />
    public async Task<bool> SendMessageAsync(string message)
    {
        await telegramService.SendMessageAsync(message);   
        
        return true;
    }
}