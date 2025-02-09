using Microsoft.AspNetCore.Mvc;
using Oid85.HomeBot.Application.Interfaces.Services;
using Oid85.HomeBot.Application.Models.Responses;
using Oid85.HomeBot.WebHost.Controller.Base;

namespace Oid85.HomeBot.WebHost.Controller;

[Route("api/[controller]")]
[ApiController]
public class JobsController(
    IJobService jobService) 
    : FinMarketBaseController
{
    /// <summary>
    /// Запустить send-notifications
    /// </summary>
    [HttpGet("run/send-notifications")]
    [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> SendNotificationsAsync() =>
        GetResponseAsync(
            jobService.SendNotificationsAsync,
            result => new BaseResponse<bool>
            {
                Result = result
            });
}