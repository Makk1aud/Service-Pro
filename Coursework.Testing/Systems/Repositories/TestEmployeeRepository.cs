using Coursework.DataApp;
using Coursework.Repository;
using Coursework.Repository.Extensions.FilterParameters;
using Coursework.Testing.Fixtures;
using Coursework.Testing.Helpers;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Testing.Systems.Repositories
{
    public class TestEmployeeRepository
    {
        [Fact]
        public void Get_OnSucces_ReturnsEmployeeList_WithCount_5()
        {
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

        [Fact]
        public void Get_OnSucces_ReturnsEmployeeList_WithParametersLastname_Count_1()
        {
            var employeeFixture = new EmployeeFixture();
            int listLenght = 1;
            var searchLastname = "шил";

            var testListOfEmployees = employeeFixture.GetDbTestEmployees();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Employee>()).Returns(testListOfEmployees);

            var employeeRepository = new EmployeeRepository(context.Object);

            var result = employeeRepository.GetEmployees(new EmployeeParameters { Lastname =searchLastname},trackChanges: false);

            result.Should().HaveCount(listLenght);
            result.Should().Contain(x => x.lastname.Contains(searchLastname,StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Get_OnSucces_ReturnsEmployeeList_WithParametersId()
        {
            var employeeFixture = new EmployeeFixture();
            int searchId = 4;

            var testListOfEmployees = employeeFixture.GetDbTestEmployees();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Employee>()).Returns(testListOfEmployees);

            var employeeRepository = new EmployeeRepository(context.Object);

            var result = employeeRepository.GetEmployees(new EmployeeParameters {Id = searchId}, trackChanges: false);

            result.Should().OnlyContain(x => x.employee_id== searchId);
        }

        [Fact]
        public void Get_OnSucces_ReturnsEmptyList_WithParametersId()
        {
            var employeeFixture = new EmployeeFixture();
            int listLenght = 0;
            int searchId = 23;

            var testListOfEmployees = employeeFixture.GetDbTestEmployees();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Employee>()).Returns(testListOfEmployees);

            var employeeRepository = new EmployeeRepository(context.Object);

            var result = employeeRepository.GetEmployees(new EmployeeParameters { Id = searchId }, trackChanges: false);

            result.Should().HaveCount(listLenght);
        }

        [Fact]
        public void Get_OnSucces_ReturnsEmployeeList_WithParametersIdAndLastname_Count_1()
        {
            var employeeFixture = new EmployeeFixture();
            int listLenght = 1;
            int searchId = 3;
            var searchLastname = "шил";

            var testListOfEmployees = employeeFixture.GetDbTestEmployees();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Employee>()).Returns(testListOfEmployees);

            var employeeRepository = new EmployeeRepository(context.Object);

            var result = employeeRepository.GetEmployees(new EmployeeParameters { Id = searchId, Lastname=searchLastname }, trackChanges: false);

            result.Should().HaveCount(listLenght);
            result.Should().OnlyContain(x => x.employee_id == searchId);
            result.Should().Contain(x => x.lastname.Contains(searchLastname, StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Get_OnSucces_ReturnEmployee_WithLoginCode()
        {
            var employeeFixture = new EmployeeFixture();
            var login_code = "1111";

            var testListOfEmployees = employeeFixture.GetDbTestEmployees();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Employee>()).Returns(testListOfEmployees);

            var employeeRepository = new EmployeeRepository(context.Object);

            var result = employeeRepository.GetEmployeeByLogin(login_code,trackChanges: false);

            result.Should().NotBeNull();           
        }

        [Fact]
        public void Get_OnSucces_ReturnNull_WithLoginCode()
        {
            var employeeFixture = new EmployeeFixture();
            var login_code = "11343211";

            var testListOfEmployees = employeeFixture.GetDbTestEmployees();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Employee>()).Returns(testListOfEmployees);

            var employeeRepository = new EmployeeRepository(context.Object);

            var result = employeeRepository.GetEmployeeByLogin(login_code, trackChanges: false);

            result.Should().BeNull();
        }

        [Fact]
        public async void Get_OnSucces_ReturnError_AddEmployee()
        {
            var employeeFixture = new EmployeeFixture();
            var saveResult = 0;
            var newEmp = new Employee
            {
                //firstname = "Максим",
                //lastname = "Алиновский",
                //email = "makklaud@mail.ru",
                login_code = "MTEfgdggfhfxMQ==",
                phone = "1234567890",
                position_id = 6
            };

            var testListOfEmployees = employeeFixture.GetDbTestEmployees();
            var listLenght = testListOfEmployees.Count();


            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Employee>()).Returns(testListOfEmployees);
            context.Setup(x => x.SaveChangesAsync()).Callback(() => 
            {
                saveResult++;
                listLenght++;
            });

            var employeeRepository = new RepositoryManager(context.Object);
            employeeRepository.Employee.CreateEmployee(newEmp);
            await employeeRepository.SaveAsync();

            var result = employeeRepository.Employee.GetEmployees(trackChanges: false);
            saveResult.Should().Be(1);
            result.Should().HaveCount(listLenght);
        }
    }
}
