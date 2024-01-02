using Coursework.DataApp;
using Coursework.Repository.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Contracts
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        PagedList<Product> GetProducts
    }
}
