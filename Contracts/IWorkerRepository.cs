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
        Task<IEnumerable<Worker>> GetWorkersAsync(bool trackChanges);
        Task<Worker> GetWorkerAsync(Guid id, bool trackChanges);
    }
}
