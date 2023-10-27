using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Infrastructure
{
    public class ActivityRepository : IActivityRepository
    {
        public Task<ActivityDefinition> CreateActivityDefinition(string name, bool multipleWorkers, int recoveryPeriod)
        {
            throw new NotImplementedException();
        }

        public Task DeleteActivityDefinition(int activityId)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityDefinition?> GetActivityDefinition(int activityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityDefinition>> GetAllActivityDefinitions()
        {
            throw new NotImplementedException();
        }

        public Task ModifyActivityDefinition(ActivityDefinition activityDefinition)
        {
            throw new NotImplementedException();
        }
    }
}