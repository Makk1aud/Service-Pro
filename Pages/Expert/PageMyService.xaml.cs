using Coursework.Classes;
using Coursework.Repository.Extensions.FilterParameters;
using Coursework.ViewModels;
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

namespace Coursework.Pages.Expert
{
    public partial class PageMyService : Page
    {
        private MetaData _metaData;
        public PageMyService()
        {
            InitializeComponent();

            ComboBoxStatus.ItemsSource = ExpertClass
                .RepositoryManager
                .ProductStatus
                .FindAllGeneric(trackChanges: false);

            ComboBoxProductType.ItemsSource = ExpertClass
                .RepositoryManager
                .ProductType
                .FindAllGeneric(trackChanges: false);

            DataGridSorting(1);
        }

        private void DataGridSorting(int pageNum)
        {
            var listOfProducts = ExpertClass
                .RepositoryManager
                .Product
                .GetProducts(new ProductParameters()
                {
                    PageNumber = pageNum,
                    SearchName = TextBoxProductName.Text,
                    ProductStatusId = Convert.ToInt32(ComboBoxStatus.SelectedValue),
                    ProductTypeId = Convert.ToInt32(ComboBoxProductType.SelectedValue),
                    ExpertId = ExpertClass.Employee.employee_id
                }, trackChanges: false);

            _metaData = listOfProducts.MetaData;
            this.DataContext = new ProductsViewModel { MetaData = _metaData, Employee = ExpertClass.Employee };

            DataGridProducts.ItemsSource = listOfProducts;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) =>
            DataGridSorting(_metaData.CurrentPage = 1);

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            DataGridSorting(_metaData.CurrentPage = 1);

        private void ButtonPreviousPage_Click(object sender, RoutedEventArgs e) =>
            DataGridSorting(_metaData.HasPrevious ? --_metaData.CurrentPage : _metaData.CurrentPage);

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e) =>
            DataGridSorting(_metaData.HasNext ? ++_metaData.CurrentPage : _metaData.CurrentPage);

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            TextBoxProductName.Text = string.Empty;
            ComboBoxProductType.SelectedItem = null;
            ComboBoxStatus.SelectedItem = null;
            DataGridSorting(1);
        }

        private void ButtonSelectProduct_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
