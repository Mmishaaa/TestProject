namespace Contracts
{
    public interface IRepositoryManager
    {
        IWorkerRepository Worker { get; }
        IDepartmentRepository Department { get; }
        IProductRepository Product { get; }
        Task SaveAsync();
    }
}
