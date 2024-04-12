using Contracts;
using Entities.DTO;
using Entitties;
using Entitties.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges) =>
           await FindAll(trackChanges)
            .Include(d => d.Workers)
            .OrderBy(d => d.Name)
            .ToListAsync();

        public async Task<Department?> GetDepartmentAsync(Guid id, bool trackChanges) =>
            await FindByCondition(d => d.Id.Equals(id), trackChanges)
            .Include(d => d.Workers)
            .SingleOrDefaultAsync();

        public void CreateDepartment(Department department) => Create(department);
        public async Task<IEnumerable<Department>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();

        public void DeleteDepartment(Department department) => Delete(department);


    }
}
