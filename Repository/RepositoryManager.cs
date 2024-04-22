using Contracts;
using Entitties;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        private IDepartmentRepository _departmentRepository;
        private IWorkerRepository _workerRepository;
        private IProductRepository _productRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IDepartmentRepository Department
        {
            get
            {
                if (_departmentRepository == null)
                    _departmentRepository = new DepartmentRepository(_repositoryContext);
                return _departmentRepository;
            }
        }

        public IWorkerRepository Worker
        {
            get
            {
                if (_workerRepository == null)
                    _workerRepository = new WorkerRepository(_repositoryContext);
                return _workerRepository;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);
                return _productRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
