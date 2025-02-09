using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Core.Interfaces;

public interface IScheduleChecker
{
    bool IsWorkerBusy(Worker worker, DateTimeOffset start, DateTimeOffset endIncludingRest, IEnumerable<Assignment> allAssignments);
}
