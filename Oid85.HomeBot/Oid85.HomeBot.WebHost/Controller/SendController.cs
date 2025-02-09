﻿using Microsoft.AspNetCore.Mvc;
using Oid85.HomeBot.Application.Interfaces.Services;
using Oid85.HomeBot.Application.Models.Responses;
using Oid85.HomeBot.WebHost.Controller.Base;

namespace Oid85.HomeBot.WebHost.Controller;

[Route("api/[controller]")]
[ApiController]
public class SendController(
    ISendService sendService) 
    : FinMarketBaseController
{
    /// <summary>
    /// Отправить сообщение в телеграм
    /// </summary>
    [HttpGet("send/send-message")]
    [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> SendNotificationsAsync(string message) =>
        GetResponseAsync(
            () => sendService.SendMessageAsync(message),
            result => new BaseResponse<bool>
            {
                Result = result
            });
}