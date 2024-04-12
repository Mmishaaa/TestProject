using Entitties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
