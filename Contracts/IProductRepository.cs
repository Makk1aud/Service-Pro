using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Contracts
{
    public interface IProductRepository
    {
        List<ProductType> GetProductTypes();
        void AddProduct(Product product);
    }
}
