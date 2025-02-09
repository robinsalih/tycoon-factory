namespace Tycoon.Factory.Core.Model;

public record Assignment(int Id, ActivityDefinition Activity, DateTimeOffset Start, DateTimeOffset End, IEnumerable<Worker> workers);
