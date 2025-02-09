namespace Tycoon.Factory.Api.Controllers;

public record ScheduleRequest(int ActivityType, DateTimeOffset Start, DateTimeOffset End, IEnumerable<int> Workers);
