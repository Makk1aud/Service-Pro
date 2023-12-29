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

namespace Coursework.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployyRepository
    {
        public EmployeeRepository(CourseworkEntities context) 
            : base(context)
        {
        }

        public void CreateEmployy(Employee employee) =>
            Create(employee);
        

        public void DeleteEmployee(Employee employee) =>
            Delete(employee);

        public IEnumerable<Employee> GetEmployees(bool trackChanges) =>
            GetEmployees(trackChanges);

        public IEnumerable<Employee> GetEmployeesPagination(EmployeeParameters parameters, bool trackChanges) =>
            FindAll(trackChanges)
            .FindById(parameters.Id)
            .FindByLogin(parameters.Login)
            .ToList();

        public void UpdateEmployee(Employee employee) =>
            Update(employee);
    }
}
