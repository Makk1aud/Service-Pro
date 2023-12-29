using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.DataApp;

namespace Coursework.Contracts
{
    public interface IEmployyRepository
    {
        List<Employee> GetEmployees();

        Employee GetEmployeeById(int employeeId);

        Employee GetEmployeeByLogin(string employeeLogin);

        List<Position> GetPositions();
        void DeleteEmployee(Employee employee);
    }
}
