namespace Tycoon.Factory.Core.Interfaces;

public interface IScheduler
{
    Task ScheduleActivity(int activityId, DateTimeOffset start, DateTimeOffset end, IEnumerable<int> workerIds);
}
