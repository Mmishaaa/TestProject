using Entitties.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>>GetAllDepartmentsAsync(bool trackChanges);
        Task<Department> GetDepartmentAsync(Guid id, bool trackChanges);
        void CreateDepartment(Department department);
        Task<IEnumerable<Department>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

        void DeleteDepartment(Department department);
    }
}
