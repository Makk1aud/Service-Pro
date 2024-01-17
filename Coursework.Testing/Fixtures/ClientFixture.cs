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
    public class ClientFixture : MainFixture
    {
        protected Faker<Client> GenerationClientsRules()
        {
            return new Faker<Client>("ru")
                .RuleFor(x => x.phone, f => f.Phone.PhoneNumber("+7 ###-###-##-##"))
                .RuleFor(x => x.firstname, f => f.Name.FirstName())
                .RuleFor(x => x.lastname, f => f.Name.LastName());
        }

        public FakeDbSet<Client> GetRandomDbSetClients(int count)
        {
            GenerationGeneric<Client> generation = GenerationClientsRules;
            return GetRandomDbSetList(count, generation);
        }

        public IEnumerable<Client> GetRandomListOfClients(int count) =>
            GenerationClientsRules().Generate(count);

        public FakeDbSet<Client> GetDbSetTestClients() =>
            new()
            {
                new()
                {
                    client_id = 1,
                    firstname = "Oleg",
                    lastname = "Petrov",
                    phone = "+7 934-234-12-23"
                },
                new()
                {
                    client_id = 2,
                    firstname = "Alexey",
                    lastname = "Nasedkin",
                    phone = "+7 435-345-98-23"
                },
                new()
                {
                    client_id = 3,
                    firstname = "Baurkhan",
                    lastname = "Tulembaev",
                    phone = "+7 234-543-98-98"
                },
                new()
                {
                    client_id = 4,
                    firstname = "Kirill",
                    lastname = "Shilyaev",
                    phone = "+7 874-983-93-47"
                }
            };
    }
}
