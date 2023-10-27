using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Infrastructure
{
    public class AssignmentRepository : IAssignmentRepository
    {
        public Task<Assignment> CreateAssignment(int activityId, DateTimeOffset start, DateTimeOffset end, IEnumerable<int> workerIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAssignment(int assignmentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Assignment>> GetAllAssignments()
        {
            throw new NotImplementedException();
        }

        public Task<Assignment?> GetAssignment(int assignmentId)
        {
            throw new NotImplementedException();
        }

        public Task ModifyAssignment(Assignment assignment)
        {
            throw new NotImplementedException();
        }
    }
}