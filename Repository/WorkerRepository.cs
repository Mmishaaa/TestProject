using Contracts;
using Entitties;
using Entitties.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class WorkerRepository : RepositoryBase<Worker>, IWorkerRepository
    {
        public WorkerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public async Task<IEnumerable<Worker>> GetWorkersAsync(Guid departmentId, bool trackChanges) =>
            await FindByCondition(w => w.Departments.Any(d => d.Id.Equals(departmentId)), trackChanges)
            .Include(w => w.Departments)
            .ToListAsync();
        public async Task<Worker?> GetWorkerAsync(Guid departmentId, Guid id, bool trackChanges) =>
            await FindByCondition(w => w.Departments.Any(d => d.Id.Equals(departmentId) && w.Id.Equals(id)), trackChanges)
            .Include(w => w.Departments)
            .SingleOrDefaultAsync();

        public void CreateWorkerForDepartment(Guid id, Worker worker) {
            var department = RepositoryContext.Departments
                            .FirstOrDefault(d => d.Id == id);
            worker.Departments = new List<Department> {department};
            Create(worker);
        } 
        public void DeleteWorker(Worker worker) => Delete(worker);
    }
}
