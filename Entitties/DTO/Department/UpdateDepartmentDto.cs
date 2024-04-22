using Entities.DTO.Worker;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.Department
{
    public class UpdateDepartmentDto
    {
        [Required(ErrorMessage = "Department Name is a required field.")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Department Description is a required field.")]

        public string Description { get; set; }

    }
}
