using Coursework.DataApp;
using Coursework.Repository;
using Coursework.Testing.Fixtures;
using Coursework.Testing.Helpers;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Testing.Systems.Repositories
{
    public class TestEmployeeRepository
    {
        [Fact]
        public void Get_OnSucces_EmployeeListWithCount_5()
        {
            //var testListOfEmployees = EmployeeFixture.GetRandomEmployees(5);
            var employeeFixture = new EmployeeFixture();
            int listLenght = 4;

            EmployeeFixture.GenerationGeneric<Employee> generationGeneric;
            generationGeneric = employeeFixture.GetGenerationEmployees;
            var testListOfEmployees = employeeFixture.GetRandomGenericList<Employee>(listLenght, generationGeneric);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Employee>()).Returns(testListOfEmployees);

            var employeeRepository = new EmployeeRepository(context.Object);

            var result = employeeRepository.GetEmployees(trackChanges: false);

            result.Count.Should().Be(listLenght);
        }
    }
}
