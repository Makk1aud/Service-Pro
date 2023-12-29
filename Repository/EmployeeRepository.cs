using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Contracts;
using Coursework.DataApp;
using System.Data.Entity;

namespace Coursework.Repository
{
    public class EmployeeRepository : IEmployyRepository
    {
        private static CourseworkEntities _context;
        static EmployeeRepository() => _context = ModelClass.GetContext();

        public void DeleteEmployee(Employee employee)
        {
            _context.Employee.Remove(employee);
            ModelClass.SaveDatabase();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employee.FirstOrDefault(x => x.employee_id == employeeId);
        }

        public Employee GetEmployeeByLogin(string employeeLogin)
        {
            return _context.Employee.FirstOrDefault(x => x.login_code  == employeeLogin);
        }

        public List<Employee> GetEmployees()
        {
            return _context.Employee.ToList();
        }

        public List<Position> GetPositions()
        {
            return _context.Position.ToList();
        }
    }
}
