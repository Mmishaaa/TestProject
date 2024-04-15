using Entities.DTO.Product;
using Entities.DTO.Worker;

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
