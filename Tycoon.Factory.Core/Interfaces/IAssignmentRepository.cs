using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Core.Interfaces;

public interface IAssignmentRepository
{
    Task<IEnumerable<Assignment>> GetAllAssignments();
    Task<Assignment?> GetAssignment(int assignmentId);
    Task<Assignment> CreateAssignment(int activityId, DateTimeOffset start, DateTimeOffset end, IEnumerable<int> workerIds);
    Task DeleteAssignment(int assignmentId);
    Task ModifyAssignment(Assignment assignment);
}
