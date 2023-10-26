using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;
using Tycoon.Factory.Utils;

namespace Tycoon.Factory.Core.Scheduling;

public class Scheduler : IScheduler
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IActivityRepository _activityRepository;
    private readonly IScheduleChecker _checker;

    public Scheduler(IAssignmentRepository assignmentRepository, IWorkerRepository workerRepository, IActivityRepository activityRepository, IScheduleChecker checker)
    {
        _assignmentRepository = assignmentRepository;
        _workerRepository = workerRepository;
        _activityRepository = activityRepository;
        _checker = checker;
    }

    public async Task<Assignment> ScheduleActivity(int activityId, DateTimeOffset start, DateTimeOffset end, IEnumerable<int> workerIds)
    {
        var activity = await _activityRepository.GetActivityDefinition(activityId);
        if (activity == null)
            throw new ArgumentException($"Cannot find {nameof(activityId)} {activityId}");

        var workers = await workerIds.SelectAsync(id => _workerRepository.GetWorker(id));
        if (workers.Any(w => w == null))
            throw new ArgumentException($"One or more {nameof(workerIds)} not found");

        var workerCount = workers.Count();
        if (workerCount == 0)
            throw new ArgumentException($"{nameof(workerIds)} cannot be emtpy");

        if (workerCount > 1 && !activity.MultipleWorkers)
            throw new ArgumentException($"Activity {activityId} does not allow multiple workers");

        if (end <= start)
            throw new ArgumentException("Start time must be before end time");

        var startUtc = start.ToUniversalTime();
        var endUtc = end.ToUniversalTime();

        var assignment = await _assignmentRepository.CreateAssignment(activityId, startUtc, endUtc, workerIds);
        
        return assignment;
    }
}