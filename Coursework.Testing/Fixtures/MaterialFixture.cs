using Bogus;
using Coursework.DataApp;
using Coursework.Testing.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Testing.Fixtures
{
    public class MaterialFixture : MainFixture
    {
        private Faker<Material> GetMaterialRules()
        {
            return new Faker<Material>()
                .RuleFor(x => x.material_title, f => f.Random.Word())
                .RuleFor(x => x.material_price, f => f.Random.Number(30, 30000))
                .RuleFor(x => x.mat_type_id, f => f.Random.Number(1, 100))
                .RuleFor(x => x.manufacturer_id, f => f.Random.Number(1, 100));
        }

        public FakeDbSet<Material> GetRandomDbSetMaterials(int count)
        {
            GenerationGeneric<Material> generationGeneric = GetMaterialRules;
            return GetRandomDbSetList<Material>(count, generationGeneric);
        }

        public FakeDbSet<Material> GetTestDbSetMaterials()
            => new()
            {
                new()
                {
                    material_id = 1,
                    material_title = "Экран",
                    material_price= 4334,
                    mat_type_id = 34,
                    manufacturer_id = 33
                },
                new()
                {
                    material_id = 2,
                    material_title = "Батарея",
                    material_price= 450,
                    mat_type_id = 22,
                    manufacturer_id = 33
                },
                new()
                {
                    material_id = 3,
                    material_title = "Камера",
                    material_price= 350,
                    mat_type_id = 34,
                    manufacturer_id = 21
                },
                new()
                {
                    material_id = 4,
                    material_title = "Экран",
                    material_price= 1532,
                    mat_type_id = 23,
                    manufacturer_id = 4
                },
            };
    }
}
