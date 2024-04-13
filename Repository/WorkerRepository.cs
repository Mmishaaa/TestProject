using Contracts;
using Entitties;
using Entitties.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WorkerRepository : RepositoryBase<Worker>, IWorkerRepository
    {
        public WorkerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Worker>> GetWorkersAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .Include(w => w.Departments)
            .ToListAsync();

        public async Task<Worker?> GetWorkerAsync(Guid id, bool trackChanges) =>
            await FindByCondition(w => w.Id.Equals(id), trackChanges)
            .Include(w => w.Departments)
            .SingleOrDefaultAsync();

        public void CreateWorker(Worker worker) => Create(worker);
    }
}
