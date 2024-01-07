using Coursework.Contracts;
using Coursework.DataApp;
using Coursework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Coursework.Classes
{
    public static class AdminClass
    {
        public static IRepositoryManager repositoryManager { get; }
        static AdminClass()
        {
            repositoryManager = new RepositoryManager(new CourseworkEntities());
        }
        public static Employee employee;
        public static Frame frameLeftPanel;
        public static Frame frameMainStruct;
    }
}
