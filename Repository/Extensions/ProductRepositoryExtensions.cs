using Coursework.DataApp;
using Coursework.Repository.Extensions.FilterParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository.Extensions
{
    public static class ProductRepositoryExtensions
    {
        public static IQueryable<Product> GetProductsByExpertId(this IQueryable<Product> items, int? expertId) =>
            items.Where(x => x.expert_id == expertId);

        public static IQueryable<Product> GetProductsByName(this IQueryable<Product> items, string searchName)
        {
            if(string.IsNullOrEmpty(searchName))
                return items;

            var lowerName = searchName.Trim().ToLower();
            return items.Where(x => x.product_name.ToLower().Contains(lowerName));
        }

        public static IQueryable<Product> GetProductsByDescription(this IQueryable<Product> items, string searchDesc)
        {
            if (string.IsNullOrEmpty(searchDesc))
                return items;

            var lowerName = searchDesc.Trim().ToLower();
            return items.Where(x => x.prod_description.ToLower().Contains(lowerName));
        }

        public static IQueryable<Product> GetProductsByProductTypeId(this IQueryable<Product> items, int? productTypeId) =>
            productTypeId is null || productTypeId == 0
            ? items
            : items.Where(x => x.pr_type_id == productTypeId);
    }
}
