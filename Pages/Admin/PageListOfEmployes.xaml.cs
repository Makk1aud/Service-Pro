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
using Сoursework.Core.Models;

namespace Coursework.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для PageListOfEmployes.xaml
    /// </summary>
    public partial class PageListOfEmployes : Page
    {
        private EmployeeRepository _employeeRepository;
        private List<Employee> _employees;
        public PageListOfEmployes()
        {
            InitializeComponent();
            _employeeRepository = new EmployeeRepository();
            _employees = _employeeRepository.GetEmployees();
            DataGridEmployes.ItemsSource = _employees;
            ComboBoxPosSort.ItemsSource = _employeeRepository.GetPositions();
        }

        private void DataGridSort()
        {
            var sortList = _employees;
            TextBoxSort(ref sortList);
            ComboBoxSort(ref sortList);
            DataGridEmployes.ItemsSource = sortList;
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
            DataGridEmployes.ItemsSource = _employees;
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            var emp = DataGridEmployes.SelectedItem as Employee;
            _employeeRepository.DeleteEmployee(emp);
            _employees = _employeeRepository.GetEmployees();
            DataGridSort();
        }
    }
}
