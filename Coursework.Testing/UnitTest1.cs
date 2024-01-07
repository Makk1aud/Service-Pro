using Coursework.Contracts;
using Coursework.DataApp;
using Coursework.Repository;
using Coursework.Testing.Helpers;
using FluentAssertions;
using Moq;
using System.Data.Entity;

namespace Coursework.Testing
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var listEmployees = new FakeDbSet<Employee>()
            {
                new Employee
                {
                    firstname = "Oleg",
                    lastname = "Petrov",
                    email = "makklaud@mail.ru",
                    login_code = "MTExMQ==",
                    phone = "1234567890",
                    position_id = 2
                }
            };
            //var repositorybase = new Mock<IRepositoryBase<Employee>>();
            //repositorybase.Setup(x => x.FindAll(false)).Returns(listEmployees);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Employee>()).Returns(listEmployees);

            var employeeRepository = new EmployeeRepository(context.Object);

            var result = employeeRepository.GetEmployees(trackChanges: false);

            result.Count.Should().Be(1);
        }
    }
}