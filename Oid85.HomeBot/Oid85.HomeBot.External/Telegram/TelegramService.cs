﻿using Microsoft.Extensions.Configuration;
using Oid85.HomeBot.Common.Helpers;
using Oid85.HomeBot.Common.KnownConstants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Oid85.HomeBot.External.Telegram;

/// <inheritdoc />
public class TelegramService(
    IConfiguration configuration,
    TelegramBotClient botClient) 
    : ITelegramService
{
    /// <inheritdoc />
    public async Task SendMessageAsync(string message)
    {
        string chatId = ConvertHelper.Base64Decode(
            configuration.GetValue<string>(KnownSettingsKeys.TelegramChatId)!);
        
        await botClient.SendMessage(chatId, message);
    }

    public async Task MessageHandleAsync(Message message, UpdateType type)
    {
        await SendMessageAsync("...");
    }
}