using Entities.DTO.Product;
using Entities.DTO.Worker;
using Entitties.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Department
{
    public class DepartmentDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        public IEnumerable<WorkerDtoForDepartment> Workers { get; set; }
    }
}
