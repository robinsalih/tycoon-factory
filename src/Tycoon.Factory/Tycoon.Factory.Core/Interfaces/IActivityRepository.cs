using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Core.Interfaces;

internal interface IActivityRepository
{
    Task<IEnumerable<ActivityDefinition>> GetAllActivityDefinitions();
}
