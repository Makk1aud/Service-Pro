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
    public static class ExpertClass
    {
        public static IRepositoryManager RepositoryManager { get; set;}

        public static Frame FrameLeftPanel { get; set; }
        public static Frame FrameMainStruct { get; set; }
        public static Employee Employee { get; set; }
    }
}
