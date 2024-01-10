using Bogus;
using Coursework.Testing.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Testing.Fixtures
{
    public abstract class MainFixture
    {
        protected delegate Faker<T> GenerationGeneric<T>() where T : class;

        protected FakeDbSet<T> GetRandomDbSetList<T>(int count, GenerationGeneric<T> generationMethod) where T : class
        {
            List<T> listOfEmployees = generationMethod().Generate(count);
            FakeDbSet<T> fakerList = new FakeDbSet<T>();
            foreach (var item in listOfEmployees)
                fakerList.Add(item);
            return fakerList;
        }
    }
}
