using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Core.Scheduling;

public class ScheduleChecker : IScheduleChecker
{
    public bool IsWorkerBusy(Worker worker, DateTimeOffset start, DateTimeOffset endIncludingRest, IEnumerable<Assignment> allAssignments)
    {
        throw new NotImplementedException();
    }
}
