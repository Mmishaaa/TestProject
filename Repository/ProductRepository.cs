using Contracts;
using Entitties;
using Entitties.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(Guid departmentId, bool trackChanges) =>
            await FindByCondition(p => p.DepartmentId.Equals(departmentId), trackChanges)
            .OrderBy(p => p.Name)
            .ToListAsync();

        public async Task<Product?> GetProductAsync(Guid departmentId, Guid id, bool trackChanges) =>
            await FindByCondition(p => p.DepartmentId.Equals(departmentId) && p.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateProductForDepartment(Guid deprtmentId, Product product)
        {
            product.DepartmentId = deprtmentId;
            Create(product);
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }
    }
}
