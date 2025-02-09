using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Core.Interfaces;

public interface IActivityRepository
{
    Task<IEnumerable<ActivityDefinition>> GetAllActivityDefinitions();
    Task<ActivityDefinition?> GetActivityDefinition(int activityId);
    Task<ActivityDefinition> CreateActivityDefinition(string name, bool multipleWorkers, int recoveryPeriod);
    Task DeleteActivityDefinition(int activityId);
    Task ModifyActivityDefinition(ActivityDefinition activityDefinition);
}
