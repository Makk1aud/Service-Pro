using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Contracts;
using Coursework.DataApp;
using System.Data.Entity;
using Coursework.Repository.Extensions.FilterParameters;
using Coursework.Repository.Extensions;
using Coursework.Helpers;

namespace Coursework.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        private readonly CourseworkEntities coursework;
        public EmployeeRepository(CourseworkEntities context) 
            : base(context)
        {
            coursework= context;
        }

        public void CreateEmployee(Employee employee)
        {
            employee.login_code = PasswordHash.EncodePasswordToBase64(employee.login_code);
            Create(employee);
        }           
        
        public void DeleteEmployee(Employee employee) =>
            Delete(employee);

        public Employee GetEmployeeByLogin(string login, bool trackChanges)
        {
            login = PasswordHash.EncodePasswordToBase64(login);
            return FindByCondition(x => x.login_code.Equals(login), trackChanges)
            .SingleOrDefault();
        }


        public List<Employee> GetEmployees(bool trackChanges) =>
            FindAll(trackChanges)
            .ToList();


        public List<Employee> GetEmployees(EmployeeParameters employeeParameters, bool trackChanges) =>
            FindAll(trackChanges)
            .FindById(employeeParameters.Id)
            .FindByPositionId(employeeParameters.PositonId)
            .FindByLogin(employeeParameters.Login)
            .FindByLastname(employeeParameters.Lastname)
            .ToList();


        public void UpdateEmployee(Employee employee) =>
            Update(employee);
    }
}
