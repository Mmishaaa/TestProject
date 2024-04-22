using Entitties.Models;

namespace Contracts
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetWorkersAsync(Guid deprtmentId, bool trackChanges);
        Task<Worker> GetWorkerAsync(Guid deprtmentId, Guid id, bool trackChanges);

        void CreateWorkerForDepartment(Guid deprtmentId, Worker worker);
        void DeleteWorker(Worker worker);
    }
}
