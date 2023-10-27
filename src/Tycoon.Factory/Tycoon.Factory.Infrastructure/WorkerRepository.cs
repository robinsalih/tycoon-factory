using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;

namespace Tycoon.Factory.Infrastructure
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly Dictionary<int, Worker> _data = new();
        private int _nextId = 1;
        
        public Task<Worker> CreateWorker(string name)
        {
            var id = ++_nextId;
            var worker = new Worker(id, name);
            _data.Add(id, worker);
            return Task.FromResult(worker);
        }

        // This would use a deleted flag if in a database as there might be assignments that refer to it        
        public Task DeleteWorker(int workerId)
        {
            _data.Remove(workerId);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Worker>> GetAllWorkers()
        {
            return Task.FromResult((IEnumerable<Worker>) _data.Values);
        }

        public Task<Worker?> GetWorker(int workerId)
        {
            return Task.FromResult(_data.TryGetValue(workerId, out var worker) ? worker : null);
        }

        public Task ModifyWorker(Worker worker)
        {
            _data[worker.Id] = worker;
            return Task.CompletedTask;
        }
    }
}