using Coursework.DataApp;
using Coursework.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Repository.Pagination;
using Coursework.Repository.Extensions.FilterParameters;

namespace Coursework.Repository
{
    public class ProductRepository :RepositoryBase<Product> ,IProductRepository
    {
        public ProductRepository(CourseworkEntities context) 
            : base(context)
        {
        }

        public void CreateProduct(Product product) =>
            Create(product);

        public void DeleteProduct(Product product) => 
            Delete(product);

        public PagedList<Product> GetProducts(ProductParameters productParameters, bool trackChanges)
        {
            var items = FindAll(trackChanges)                
                .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                .Take(productParameters.PageSize)
                .ToList();

            var count = FindAll(trackChanges)
                .Count();
            return new PagedList<Product>(items, count, productParameters.PageNumber, productParameters.PageSize);
        }

        public IEnumerable<Product> GetProducts(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(x => x.product_name)
            .ToList();

        public void UpdateProduct(Product product) => 
            Update(product);
    }
}
