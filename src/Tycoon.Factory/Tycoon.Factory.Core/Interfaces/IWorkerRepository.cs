using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Core.Interfaces;

public interface IWorkerRepository
{
    Task<IEnumerable<Worker>> GetAllWorkers();
    Task<Worker?> GetWorker(int workerId);
    Task<Worker> CreateWorker(string name);
    Task DeleteWorker(int workerId);
    Task ModifyWorker(Worker worker);
}
