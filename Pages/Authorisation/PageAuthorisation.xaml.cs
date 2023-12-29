using Coursework.Classes;
using Coursework.DataApp;
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
using Сoursework.Core.Interfaces;
using Сoursework.Core.Models;

namespace Coursework.Pages.Authorisation
{
    /// <summary>
    /// Логика взаимодействия для PageAuthorisation.xaml
    /// </summary>
    public partial class PageAuthorisation : Page
    {
        private IEmployyRepository _repository;
        public PageAuthorisation()
        {
            InitializeComponent();
            _repository = new EmployeeRepository();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            var emp = _repository.GetEmployeeByLogin(PasswordBoxCode.Password);
            if(emp == null)
            {
                return;
            }
            switch(emp.position_id)
            {
                case 1:
                    ChangeWindows.Change(Window.GetWindow(this), new AdminWindow(emp));
                    break;
                default:
                    ChangeWindows.Change(Window.GetWindow(this), new ExpertWindow(emp));
                    break;
            }
        }
    }
}
