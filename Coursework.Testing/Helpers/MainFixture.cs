using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Testing.Helpers
{
    public abstract class MainFixture
    {
        public delegate Faker<T> GenerationGeneric<T>() where T : class;

        public FakeDbSet<T> GetRandomGenericList<T>(int count, GenerationGeneric<T> generationMethod) where T : class
        {
            List<T> listOfEmployees = generationMethod().Generate(count);
            FakeDbSet<T> fakerList = new FakeDbSet<T>();
            foreach (var item in listOfEmployees)
                fakerList.Add(item);
            return fakerList;
        }
    }
}
