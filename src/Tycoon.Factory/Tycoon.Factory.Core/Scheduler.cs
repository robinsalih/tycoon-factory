using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Core;

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

    public Task<Assignment> ScheduleActivity(int activityId, DateTimeOffset start, DateTimeOffset end, IEnumerable<int> workerIds)
    {
        throw new NotImplementedException();
    }
}