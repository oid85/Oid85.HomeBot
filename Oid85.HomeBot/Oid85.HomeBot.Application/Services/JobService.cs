using Oid85.HomeBot.Application.Interfaces.Services;

namespace Oid85.HomeBot.Application.Services;

public class JobService(
    ISendService sendService) 
    : IJobService
{
    public async Task<bool> SendNotificationsAsync()
    {
        return true;
    }
}