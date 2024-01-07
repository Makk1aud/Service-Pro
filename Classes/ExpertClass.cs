using Coursework.Contracts;
using Coursework.DataApp;
using Coursework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Classes
{
    public static class ExpertClass
    {
        public static IRepositoryManager repositoryManager { get; }
        static ExpertClass()
        {
            repositoryManager = new RepositoryManager(new CourseworkEntities());
        }
        public static Employee employee;
    }
}
