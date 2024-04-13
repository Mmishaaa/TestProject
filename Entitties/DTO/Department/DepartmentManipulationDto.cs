using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTO.Product;

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
    }
}
