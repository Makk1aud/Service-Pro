using Coursework.Contracts;
using Coursework.DataApp;
using Coursework.Repository.Extensions;
using Coursework.Repository.Extensions.FilterParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository
{
    public class MaterialRepository : RepositoryBase<Material>, IMaterialRepository
    {
        public MaterialRepository(CourseworkEntities context)
            : base(context)
        {
        }

        public void CreateMaterial(Material material) =>
            Create(material);

        public Material GetMaterial(int materialId, bool trackChanges) =>
            FindByCondition(x => x.material_id == materialId, trackChanges)
            .SingleOrDefault();

        public IEnumerable<Material> GetMaterials(MaterialParameters parameters, bool trackChanges) =>
            FindAll(trackChanges)
            .GetMaterialByName(parameters.MaterialTitle)
            .GetMaterialByMaterialTypeId(parameters.MaterialTypeId)
            .GetMaterialByPrice(parameters.MinPrice, parameters.MaxPrice)
            .ToList();


        public void UpdateMaterial(Material material) => 
            Update(material);
    }
}
