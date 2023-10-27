using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Infrastructure
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly Dictionary<int, ActivityDefinition> _data = new();
        private int _nextId = 1;

        public Task<ActivityDefinition> CreateActivityDefinition(string name, bool multipleWorkers, int restPeriod)
        {
            var id = ++_nextId;
            var activity = new ActivityDefinition(id, name, multipleWorkers, TimeSpan.FromHours(restPeriod));
            _data.Add(id, activity);
            return Task.FromResult(activity);

        }

        public Task DeleteActivityDefinition(int activityId)
        {
            _data.Remove(activityId);
            return Task.CompletedTask;
        }

        public Task<ActivityDefinition?> GetActivityDefinition(int activityId)
        {
            return Task.FromResult(_data.TryGetValue(activityId, out var activity) ? activity : null);
        }

        public Task<IEnumerable<ActivityDefinition>> GetAllActivityDefinitions()
        {
            return Task.FromResult((IEnumerable<ActivityDefinition>)_data.Values);
        }

        public Task ModifyActivityDefinition(ActivityDefinition activityDefinition)
        {
            _data[activityDefinition.Id] = activityDefinition;
            return Task.CompletedTask;
        }
    }
}