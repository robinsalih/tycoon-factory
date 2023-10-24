namespace Tycoon.Factory.Core.Model;

public record Assignment(int Id, int ActivityId, DateTimeOffset Start, DateTimeOffset End, IEnumerable<int> WorkerIds);
