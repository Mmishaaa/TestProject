using Contracts;
using Entitties;
using Entitties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WorkerRepository : RepositoryBase<Worker>, IWorkerRepository
    {
        public WorkerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {       
        }
    }
}
