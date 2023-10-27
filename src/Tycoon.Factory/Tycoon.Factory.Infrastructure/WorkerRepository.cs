using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Infrastructure
{
    public class WorkerRepository : IWorkerRepository
    {
        public Task<Worker> CreateWorker(string name)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorker(int workerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Worker>> GetAllWorkers()
        {
            throw new NotImplementedException();
        }

        public Task<Worker?> GetWorker(int workerId)
        {
            throw new NotImplementedException();
        }

        public Task ModifyWorker(Worker worker)
        {
            throw new NotImplementedException();
        }
    }
}