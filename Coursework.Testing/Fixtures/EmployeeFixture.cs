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
        protected Faker<Employee> GenerationEmployeesRules()
        {
            return new Faker<Employee>("ru")
                .RuleFor(x => x.employee_id, f => f.Random.Number(1,1000))
                .RuleFor(x => x.firstname, f => f.Name.FirstName())
                .RuleFor(x => x.lastname, f => f.Name.LastName())
                .RuleFor(x => x.email, f => f.Internet.Email())
                .RuleFor(x => x.login_code, f => f.Random.Word())
                .RuleFor(x => x.phone, f => f.Phone.PhoneNumber("+7 ###-###-##-##"))
                .RuleFor(x => x.position_id, f => f.Random.Number(1, 2));            
        }

        public FakeDbSet<Employee> GetDbSetRandomEmployee(int listLength)
        {
            EmployeeFixture.GenerationGeneric<Employee> generationGeneric;
            generationGeneric = GenerationEmployeesRules;
            return GetRandomDbSetList<Employee>(listLength, generationGeneric);
        }

        public IEnumerable<Employee> GetListOfRandomEmployees(int listLength)
        {
            var listEmp = GenerationEmployeesRules().Generate(listLength);
            return listEmp;
        }

        public FakeDbSet<Employee> GetDbSetTestEmployees() =>
            new ()
            {
                new()
                {
                    employee_id = 1,
                    firstname = "Олег",
                    lastname = "Петров",
                    email = "makklaud@mail.ru",
                    login_code = "MTExMQ==",
                    phone = "1234567890",
                    position_id = 2
                },
                new()
                {
                    employee_id = 2,
                    firstname = "Алексей",
                    lastname = "Наседкин",
                    email = "makklaud@mail.ru",
                    login_code = "MTdsExgfhgfggfMQ==",
                    phone = "1234567890",
                    position_id = 1
                },
                new()
                {
                    employee_id = 3,
                    firstname = "Кирилл",
                    lastname = "Шиляев",
                    email = "makklaud@mail.ru",
                    login_code = "MTExMfdfhggfdQ==",
                    phone = "1234567890",
                    position_id = 2
                },
                new()
                {
                    employee_id = 4,
                    firstname = "Андрей",
                    lastname = "Куличенко",
                    email = "makklaud@mail.ru",
                    login_code = "MTExMQfdsddf==",
                    phone = "1234567890",
                    position_id = 2
                },
                new()
                {
                    employee_id = 5,
                    firstname = "Максим",
                    lastname = "Алиновский",
                    email = "makklaud@mail.ru",
                    login_code = "MTEfgdggfhfxMQ==",
                    phone = "1234567890",
                    position_id = 2
                },
                new()
                {
                    employee_id = 6,
                    firstname = "Батырхан",
                    lastname = "Тюлембаев",
                    email = "makklaud@mail.ru",
                    login_code = "MTExMQghgf==",
                    phone = "1234567890",
                    position_id = 2
                },
            };
    }
}
