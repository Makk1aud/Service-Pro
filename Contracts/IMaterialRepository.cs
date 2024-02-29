using Coursework.DataApp;
using Coursework.Repository.Extensions.FilterParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Contracts
{
    public interface IMaterialRepository
    {
        IEnumerable<Material> GetMaterials(MaterialParameters parameters, bool trackChanges);
        void CreateMaterial(Material material);
        void UpdateMaterial(Material material);
        Material GetMaterial(int materialId, bool trackChanges);
    }
}
