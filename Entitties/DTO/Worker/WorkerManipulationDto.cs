using Entities.DTO.Department;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Worker
{
    public class WorkerManipulationDto
    {
        [Required(ErrorMessage = "FirstName is a required field.")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is a required field.")]
        public string LastName { get; set; }

        [Range(1, 100, ErrorMessage = "Age is required and it must be greater than 1 and less than 100")]
        public int Age { get; set; }

       // public IEnumerable<CreateDepartmentDtoForWorker> Departments { get; set; }
    }
}
