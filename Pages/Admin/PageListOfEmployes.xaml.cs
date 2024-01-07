using Coursework.Classes;
using Coursework.DataApp;
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

namespace Coursework.Pages.Admin
{
    public partial class PageListOfEmployes : Page
    {
        //private List<Employee> _employees;
        public PageListOfEmployes()
        {
            InitializeComponent();
            //_employeeRepository = new EmployeeRepository();
            //_employees = _employeeRepository.GetEmployees();
            DataGridEmployes.ItemsSource = AdminClass
                .repositoryManager
                .Employee
                .GetEmployees(trackChanges: false)
                .ToList();
            //ComboBoxPosSort.ItemsSource = _employeeRepository.GetPositions();
        }

        private void DataGridSort()
        {
            //var sortList = _employees;
            //TextBoxSort(ref sortList);
            //ComboBoxSort(ref sortList);
            DataGridEmployes.ItemsSource = AdminClass
                .repositoryManager
                .Employee
                .GetEmployees(new EmployeeParameters
                {
                    Lastname = TextBoxLastName.Text,
                    //PositonId = Convert.ToInt32(ComboBoxPosSort.SelectedValue),
                    PositonId = 0
                }, trackChanges: false);
        }

        private void TextBoxSort(ref List<Employee> list)
        {
            if (!string.IsNullOrEmpty(TextBoxLastName.Text))
                list = list.Where(x => x.lastname.ToLower().StartsWith(TextBoxLastName.Text.ToLower())).ToList();
        }

        private void ComboBoxSort(ref List<Employee> list)
        {
            if (ComboBoxPosSort.SelectedItem != null)
                list = list.Where(x => x.position_id == Convert.ToInt32(ComboBoxPosSort.SelectedValue)).ToList();
        }

        private void TextBoxLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridSort();
        }

        private void ComboBoxPosSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridSort();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            DataGridEmployes.ItemsSource = AdminClass
                .repositoryManager
                .Employee
                .GetEmployees(trackChanges: false);
        }

        private async void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            var employee = DataGridEmployes.SelectedItem as Employee;

            AdminClass.repositoryManager.Employee.DeleteEmployee(employee);
            await AdminClass.repositoryManager.SaveAsync();
            DataGridSort();
        }
    }
}
