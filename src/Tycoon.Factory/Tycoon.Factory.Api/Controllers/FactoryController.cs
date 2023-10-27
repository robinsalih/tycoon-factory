using Microsoft.AspNetCore.Mvc;
using Tycoon.Factory.Core.Interfaces;

namespace Tycoon.Factory.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FactoryController : ControllerBase
{
    private readonly IScheduler _scheduler;
    
    // Normally would not call these directly from controller
    private readonly IWorkerRepository _workerRepository;
    private readonly IActivityRepository _activityRepository;
    private readonly IAssignmentRepository _assignmentRepository;

    public FactoryController(IScheduler scheduler, IWorkerRepository workerRepository, IActivityRepository activityRepository, IAssignmentRepository assignmentRepository)
    {
        _scheduler = scheduler;
        _workerRepository = workerRepository;
        _activityRepository = activityRepository;
        _assignmentRepository = assignmentRepository;
    }

    [HttpPost("api/schedule")]
    public async Task<IActionResult> Schedule([FromBody] ScheduleRequest request)
    {
        var result = await _scheduler.ScheduleActivity(request.ActivityType, request.Start, request.End, request.Workers);
        return Ok(result);
    }

    [HttpGet("api/worker")]
    public async Task<IActionResult> GetWorkers()
    {
        var result = await _workerRepository.GetAllWorkers();
        return Ok(result);
    }

    [HttpGet("api/assignment")]
    public async Task<IActionResult> GetAssignments()
    {
        var result = await _assignmentRepository.GetAllAssignments();
        return Ok(result);
    }

    [HttpGet("api/activity")]
    public async Task<IActionResult> GetActivities()
    {
        var result = await _activityRepository.GetAllActivityDefinitions();
        return Ok(result);
    }
}