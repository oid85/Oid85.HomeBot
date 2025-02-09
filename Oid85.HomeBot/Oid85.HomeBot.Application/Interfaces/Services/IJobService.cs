namespace Oid85.HomeBot.Application.Interfaces.Services;

/// <summary>
/// Сервис задач по расписанию
/// </summary>
public interface IJobService
{
    Task<bool> SendNotificationsAsync();
}