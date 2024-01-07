using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.DataApp;
using Coursework.Repository.Extensions.FilterParameters;

namespace Coursework.Contracts
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees(bool trackChanges);
        List<Employee> GetEmployees(EmployeeParameters parameters,bool trackChanges);
        void UpdateEmployee(Employee employee);
        void CreateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Employee GetEmployeeByLogin(string login, bool trackChanges);
    }
}
