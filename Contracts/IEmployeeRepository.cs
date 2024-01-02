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
        IEnumerable<Employee> GetEmployees(bool trackChanges);
        IEnumerable<Employee> GetFilterEmployees(EmployeeParameters parameters,bool trackChanges);
        void UpdateEmployee(Employee employee);
        void CreateEmployy(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
