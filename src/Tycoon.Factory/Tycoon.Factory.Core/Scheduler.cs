using Tycoon.Factory.Core.Interfaces;

namespace Tycoon.Factory.Core;

public class Scheduler : IScheduler
{
    public Task ScheduleActivity(int activityId, DateTimeOffset start, DateTimeOffset end, IEnumerable<int> workerIds)
    {
        throw new NotImplementedException();
    }
}