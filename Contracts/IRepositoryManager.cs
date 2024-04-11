using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IWorkerRepository Worker { get; }
        IDepartmentRepository Department { get; }
        IProductRepository Product { get; }
        void Save();
    }
}
