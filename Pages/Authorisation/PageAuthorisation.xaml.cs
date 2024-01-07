using Coursework.Classes;
using Coursework.DataApp;
using Coursework.Helpers;
using Coursework.Repository.Extensions.FilterParameters;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework.Pages.Authorisation
{
    public partial class PageAuthorisation : Page
    {
        public PageAuthorisation()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            var emp = AuthorisationClass
                .repositoryManager
                .Employee
                .GetEmployeeByLogin(PasswordBoxCode.Password, trackChanges : false);

            if (emp == null)            
                return;  
            
            switch(emp.position_id)
            {
                case 1:
                    ChangeWindows.Change(Window.GetWindow(this), new ExpertWindow(emp));
                    break;
                default:
                    ChangeWindows.Change(Window.GetWindow(this), new AdminWindow(emp));
                    break;
            }
        }
    }
}
