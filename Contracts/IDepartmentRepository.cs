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
        IEnumerable<Department> GetAllDepartments(bool trackChanges);
        Department GetDepartment(Guid id, bool trackChanges);
        void CreateDepartment(Department department);
        IEnumerable<Department> GetByIds(IEnumerable<Guid> ids, bool trackChanges);

        void DeleteDepartment(Department department);
    }
}
