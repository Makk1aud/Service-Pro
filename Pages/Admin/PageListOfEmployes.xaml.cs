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
        public PageListOfEmployes()
        {
            InitializeComponent();

            DataGridSort();
            ComboBoxPosSort.ItemsSource = AdminClass
                .RepositoryManager
                .Position
                .FindAllGeneric(trackChanges: true);
        }

        private void DataGridSort()
        {
            DataGridEmployes.ItemsSource = AdminClass
                .RepositoryManager
                .Employee
                .GetEmployees(new EmployeeParameters
                {
                    Lastname = TextBoxLastName.Text,
                    PositonId = Convert.ToInt32(ComboBoxPosSort.SelectedValue)
                }, trackChanges: true);
        }

        private void TextBoxLastName_TextChanged(object sender, TextChangedEventArgs e) =>
            DataGridSort();

        private void ComboBoxPosSort_SelectionChanged(object sender, SelectionChangedEventArgs e) => 
            DataGridSort();


        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            TextBoxLastName.Text = string.Empty;
            ComboBoxPosSort.SelectedItem = null;
        }

        private async void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            var employee = DataGridEmployes.SelectedItem as Employee;
           
            AdminClass.RepositoryManager.Employee.DeleteEmployee(employee);
            await AdminClass.RepositoryManager.SaveAsync();
            DataGridSort();
        }

        private void ButtonAddEmploye_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.Navigate(new PageAddEmploye());
    }
}
