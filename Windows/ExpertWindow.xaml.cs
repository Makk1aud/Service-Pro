using Coursework.Classes;
using Coursework.DataApp;
using Coursework.Pages.Expert;
using Coursework.Pages.General;
using Coursework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Coursework
{
    public partial class ExpertWindow : Window
    {
        public ExpertWindow(Employee employee)
        {
            InitializeComponent();
            ExpertClass.Employee = employee;
            ExpertClass.RepositoryManager = new RepositoryManager(new CourseworkEntities());
            ExpertClass.FrameMainStruct = FrameMainStruct;
            ExpertClass.FrameLeftPanel = FrameLeftPanel;
            FrameMainStruct.Navigate(new PageMyService());
            FrameLeftPanel.Navigate(new PageExpertLeftPanel());
        }
    }
}
