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
    public class ProductFixture : MainFixture
    {
        public Faker<Product> GetProductsRules()
        {
            return new Faker<Product>()
                .RuleFor(x => x.product_name, f => f.Random.Word())
                .RuleFor(x => x.prod_description, f => f.Company.CatchPhrase())
                .RuleFor(x => x.pr_type_id, f => f.Random.Number(1, 10000))
                .RuleFor(x => x.client_id, f => f.Random.Number(1, 10000))
                .RuleFor(x => x.expert_id, f => f.Random.Number(1, 10000))
                .RuleFor(x => x.pr_status_id, f => f.Random.Number(1, 10000));
        }

        public FakeDbSet<Product> GetRandomDbSetProducts(int count)
        {
            GenerationGeneric<Product> generation = GetProductsRules;
            return GetRandomDbSetList<Product>(count, generation);
        }

        public FakeDbSet<Product> GetTestDbSetProducts() =>
            new()
            {
                new()
                {
                    product_id= 1,
                    product_name = "FGD34",
                    prod_description = "авпввпавава",
                    pr_type_id = 23,
                    client_id = 546,
                    expert_id = 9846,
                    pr_status_id = 2
                },
                new()
                {
                    product_id= 2,
                    product_name = "S3434",
                    prod_description = "Не работает экран",
                    pr_type_id = 34,
                    client_id = 435,
                    expert_id = 342,
                    pr_status_id = 2
                },
                new()
                {
                    product_id= 3,
                    product_name = "S3434",
                    prod_description = "Сломался",
                    pr_type_id = 32,
                    client_id = 433,
                    expert_id = 324,
                    pr_status_id = 1
                }
            };
    }
}
