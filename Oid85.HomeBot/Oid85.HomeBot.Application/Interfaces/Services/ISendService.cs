namespace Oid85.HomeBot.Application.Interfaces.Services;

/// <summary>
/// Сервис рассылки
/// </summary>
public interface ISendService
{
    /// <summary>
    /// Отправить оповещения
    /// </summary>
    Task<bool> SendMessageAsync(string message);
}