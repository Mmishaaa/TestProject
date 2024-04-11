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
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
               
        }

        public IEnumerable<Department> GetAllDepartments(bool trackChanges) => 
            FindAll(trackChanges)
            .OrderBy(d => d.Name)
            .ToList();
    }
}
