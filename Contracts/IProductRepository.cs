using Entitties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(Guid deprtmentId, bool trackChanges);
        Product GetProduct(Guid deprtmentId, Guid id, bool trackChanges);

        void CreateProductForDepartment(Guid deprtmentId, Product product);
        void DeleteProduct(Product product);
    }
}
