using Entitties.Models;

namespace Entities.Models
{
    public class DepartmentWorker
    {
        public Guid DepartmentsId { get; set; }
        public Department Department { get; set; }


        public Guid WorkersId { get; set; }
        public Worker Worker { get; set; }
    }
}
