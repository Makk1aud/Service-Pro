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

        public PagedList<Product> GetProductsPagination(ProductParameters parameters, bool trackChanges)
        {
            var items = FindAll(trackChanges)
                .ToList();

            var count = FindAll(trackChanges)
                .Count();
            return new PagedList<Product>(items, count, parameters.PageNumber, parameters.PageSize);
        }

        public void UpdateProduct(Product product) => 
            Update(product);
    }
}
