using Microsoft.AspNetCore.Mvc;
using Tycoon.Factory.Core.Interfaces;

namespace Tycoon.Factory.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FactoryController : ControllerBase
{
    private readonly IScheduler _scheduler;

    public FactoryController(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    [HttpPost("api/schedule")]
    public async Task<IActionResult> Schedule([FromBody] ScheduleRequest request)
    {
        var result = await _scheduler.ScheduleActivity(request.ActivityType, request.Start, request.End, request.Workers);
        return Ok(result);
    }
}