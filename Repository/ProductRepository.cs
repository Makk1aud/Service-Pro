using Coursework.DataApp;
using Coursework.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository
{
    public class ProductRepository : IProductRepository
    {
        static private CourseworkEntities _context;
        static ProductRepository()
        {
            _context = ModelClass.GetContext();
        }

        public void AddProduct(Product product)
        {
            _context.Product.Add(product);
            ModelClass.SaveDatabase();
        }

        public List<ProductType> GetProductTypes()
        {
            return _context.ProductType.ToList();
        }
    }
}
