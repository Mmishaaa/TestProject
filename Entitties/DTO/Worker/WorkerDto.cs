using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTO.Department;

namespace Entities.DTO.Worker
{
    public class WorkerDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public IEnumerable<DepartmentDtoForWorker> Departments { get; set; }
    }
}
