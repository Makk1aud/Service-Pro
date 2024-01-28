using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository.Extensions
{
    public static class MaterialRepositoryExtensions
    {
        public static IQueryable<Material> GetMaterialByName(this IQueryable<Material> items,string materialTitle)
        {
            if(String.IsNullOrEmpty(materialTitle))
                return items;

            var lowerName = materialTitle.Trim().ToLower();
            return items.Where(x => x.material_title.ToLower().Trim().Contains(lowerName));
        }

        public static IQueryable<Material> GetMaterialByMaterialTypeId(this IQueryable<Material> items, int? materialTypeId) =>
            materialTypeId is null || materialTypeId == 0
            ? items
            : items.Where(x => x.mat_type_id == materialTypeId);

        public static IQueryable<Material> GetMaterialByPrice(this IQueryable<Material> items, int? minPrice, int? maxPrice) =>
            minPrice < maxPrice 
            ? items.Where(x => (int)x.material_price >= minPrice && (int)x.material_price <= maxPrice)
            : items;
    }
}
