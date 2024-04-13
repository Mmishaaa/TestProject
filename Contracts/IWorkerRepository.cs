using Entitties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
