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
    public class EmployeeFixture : MainFixture
    {
        public Faker<Employee> GetGenerationEmployees()
        {
            return new Faker<Employee>("ru")
                .RuleFor(x => x.employee_id, f => f.Random.Number(1,1000))
                .RuleFor(x => x.firstname, f => f.Name.FirstName())
                .RuleFor(x => x.lastname, f => f.Name.LastName())
                .RuleFor(x => x.email, f => f.Internet.Email())
                .RuleFor(x => x.login_code, f => f.Random.Word())
                .RuleFor(x => x.phone, f => f.Phone.PhoneNumberFormat())
                .RuleFor(x => x.position_id, f => f.Random.Number(1, 2));            
        }

        //Сделать этот метод generic
        //public FakeDbSet<Employee> GetRandomEmployees(int count)
        //{
        //    List<Employee> listOfEmployees = GetGenerationEmployees().Generate(count);
        //    FakeDbSet<Employee> fakerList = new FakeDbSet<Employee>();
        //    foreach (var item in listOfEmployees)
        //        fakerList.Add(item);
        //    return fakerList;
        //}
    }
}
