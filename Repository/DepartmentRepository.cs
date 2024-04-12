using Contracts;
using Entitties;
using Entitties.Models;

namespace Repository
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public IEnumerable<Department> GetAllDepartments(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(d => d.Name)
            .ToList();

        public Department? GetDepartment(Guid id, bool trackChanges) =>
            FindByCondition(d => d.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void CreateDepartment(Department department) => Create(department);
        public IEnumerable<Department> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();

        public void DeleteDepartment(Department department) => Delete(department);
    }
}
