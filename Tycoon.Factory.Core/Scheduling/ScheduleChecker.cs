using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Core.Scheduling;

public class ScheduleChecker : IScheduleChecker
{
    public bool IsWorkerBusy(Worker worker, DateTimeOffset start, DateTimeOffset endIncludingRest, IEnumerable<Assignment> allAssignments)
    {
        var assignmentsForWorker = allAssignments.Where(a => a.workers.Contains(worker));
        var workerBusyOrResting = assignmentsForWorker.Select(a => (a.Start, a.End + a.Activity.RestPeriod));
        var overlappingAssigments = workerBusyOrResting.Where(x => TimesOverlap(x.Start, x.Item2, start, endIncludingRest));
        return overlappingAssigments.Any();
    }

    private static bool TimesOverlap(DateTimeOffset start1, DateTimeOffset end1, DateTimeOffset start2, DateTimeOffset end2)
    {
        return start1 < end2 && end1 > start2;
    }
}
