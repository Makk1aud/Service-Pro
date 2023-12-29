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
        public static IQueryable<Employee> FindByLogin(this IQueryable<Employee> items, string login)
        {
            if(String.IsNullOrWhiteSpace(login))
                return items;
            items = items.Where(x => x.login_code.Equals(login, StringComparison.CurrentCultureIgnoreCase));
            return items;
        }

        public static IQueryable<Employee> FindById(this IQueryable<Employee> items, int? id)
        {
            if (id is null)
                return items;
            items = items.Where(x => x.employee_id.Equals(id));
            return items;
        }
    }
}
