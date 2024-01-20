using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository.Extensions
{
    public static class EmployeeRepositoryExtensions
    {
        public static IQueryable<Employee> FindByLogin(this IQueryable<Employee> items, string login) =>
            String.IsNullOrWhiteSpace(login) 
            ? items
            : items.Where(x => x.login_code.Contains(login));        

        public static IQueryable<Employee> FindById(this IQueryable<Employee> items, int? employeeId) =>
            employeeId is null 
            ? items
            : items.Where(x => x.employee_id == employeeId);        

        public static IQueryable<Employee> FindByPositionId(this IQueryable<Employee> items, int? positionId) =>
            positionId is null
            ? items
            : items.Where(x => x.position_id == positionId);        

        public static IQueryable<Employee> FindByLastname(this IQueryable<Employee> items, string lastname)
        {
            if (String.IsNullOrWhiteSpace(lastname))
                return items;

            var searchLastname = lastname.Trim().ToLower();
            return items.Where(x => x.lastname.ToLower().Contains(searchLastname));
        }
    }
}
