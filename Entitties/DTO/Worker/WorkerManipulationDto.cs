using Entities.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Worker
{
    public class WorkerManipulationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

       // public IEnumerable<CreateDepartmentDtoForWorker> Departments { get; set; }
    }
}
