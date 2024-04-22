using Entitties.Models;

namespace Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(Guid deprtmentId, bool trackChanges);
        Task<Product> GetProductAsync(Guid deprtmentId, Guid id, bool trackChanges);

        void CreateProductForDepartment(Guid deprtmentId, Product product);
        void DeleteProduct(Product product);
    }
}
