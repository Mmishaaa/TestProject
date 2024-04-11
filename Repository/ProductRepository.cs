using Contracts;
using Entitties;
using Entitties.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
