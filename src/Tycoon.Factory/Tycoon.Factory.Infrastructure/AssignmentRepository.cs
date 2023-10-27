using System.Xml.Linq;
using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;
using Tycoon.Factory.Utils;

namespace Tycoon.Factory.Infrastructure
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly Dictionary<int, Assignment> _data = new();
        private int _nextId = 1;

        public AssignmentRepository(IWorkerRepository workerRepository, IActivityRepository activityRepository)
        {
            _workerRepository = workerRepository;
            _activityRepository = activityRepository;
        }


        public async Task<Assignment> CreateAssignment(int activityId, DateTimeOffset start, DateTimeOffset end, IEnumerable<int> workerIds)
        {
            var id = _nextId++;
            var activity = await _activityRepository.GetActivityDefinition(activityId);
            var workers = await workerIds.SelectAsync(id => _workerRepository.GetWorker(id));
            var assignment = new Assignment(id, activity, start, end, workers);
            _data.Add(id, assignment);
            return assignment;

        }

        public Task DeleteAssignment(int assignmentId)
        {
            _data.Remove(assignmentId);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Assignment>> GetAllAssignments()
        {
            return Task.FromResult((IEnumerable<Assignment>)_data.Values);
        }

        public Task<Assignment?> GetAssignment(int assignmentId)
        {
            return Task.FromResult(_data.TryGetValue(assignmentId, out var assignment) ? assignment : null);
        }

        public Task ModifyAssignment(Assignment assignment)
        {
            _data[assignment.Id] = assignment;
            return Task.CompletedTask;
        }
    }
}