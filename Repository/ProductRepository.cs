using Contracts;
using Entitties;
using Entitties.Models;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Product> GetProducts(Guid departmentId, bool trackChanges) =>
            FindByCondition(p => p.DepartmentId.Equals(departmentId), trackChanges)
            .OrderBy(p => p.Name);

        public Product? GetProduct(Guid departmentId, Guid id, bool trackChanges) =>
            FindByCondition(p => p.DepartmentId.Equals(departmentId) && p.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void CreateProductForDepartment(Guid deprtmentId, Product product)
        {
            product.DepartmentId = deprtmentId;
            Create(product);
        }
    }
}
