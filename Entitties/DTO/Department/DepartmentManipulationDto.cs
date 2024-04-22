using System.ComponentModel.DataAnnotations;
using Entities.DTO.Worker;

namespace Entities.DTO.Department
{
    public class DepartmentManipulationDto
    {
        [Required(ErrorMessage = "Department Name is a required field.")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Department Description is a required field.")]

        public string Description { get; set; }

        [Required(ErrorMessage = "Products (stored in Department) is a required field.")]


        public IEnumerable<CreateProductDto> Products { get; set; }

        [Required(ErrorMessage = "Workers is a required field.")]
        public IEnumerable<CreateWorkerDtoForDepartment> Workers { get; set; }
    }
}
