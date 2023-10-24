namespace Tycoon.Factory.Core.Model;

public record Assignment(int Id, ActivityDefinition activity, DateTimeOffset Start, DateTimeOffset End, IEnumerable<Worker> workers);
