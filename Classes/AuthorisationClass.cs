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
    public static class AuthorisationClass
    {
        public static IRepositoryManager RepositoryManager { get; }
        static AuthorisationClass()
        {
            RepositoryManager = new RepositoryManager(new CourseworkEntities());
        }
        public static Frame FrameAuth { get; set; }
    }
}
