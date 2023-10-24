using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Core.Interfaces;

public interface IScheduler
{
    Task<Assignment> ScheduleActivity(int activityId, DateTimeOffset start, DateTimeOffset end, IEnumerable<int> workerIds);
}
