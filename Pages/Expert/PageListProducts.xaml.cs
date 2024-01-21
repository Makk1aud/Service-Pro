using Coursework.Classes;
using Coursework.DataApp;
using Coursework.Pages.Expert;
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

namespace Coursework.Pages.General
{
    public partial class PageListProducts : Page
    {
        private int _currentPage = 1;
        private MetaData _metaData;
        public PageListProducts()
        {
            InitializeComponent();         
            DataGridProducts.ItemsSource = DefaultList(_currentPage);
            ComboBoxProductType.ItemsSource = ExpertClass
                .RepositoryManager
                .ProductType
                .FindAllGeneric(trackChanges: false);
        }

        private IEnumerable<Product> DefaultList(int pageNumber) =>
            ExpertClass
            .RepositoryManager
            .Product
            .GetProducts(new ProductParameters()
            {
                PageNumber = pageNumber
            }, trackChanges: true);

        private async void ButtonSelectProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as Product;
            product.expert_id = ExpertClass.Employee.employee_id;
            await ExpertClass.RepositoryManager.SaveAsync();
            DataGridProducts.ItemsSource = DefaultList(_currentPage);
        }

        private void DataGridSorting(int pageNum)
        {
            DataGridProducts.ItemsSource = ExpertClass
                .RepositoryManager
                .Product
                .GetProducts (new ProductParameters()
                {
                    PageNumber = pageNum,
                    SearchName = TextBoxProductName.Text,
                    SearchDesc = TextBoxProductDesc.Text,
                    ProductTypeId = Convert.ToInt32(ComboBoxProductType.SelectedValue)
                }, trackChanges: true);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) =>
            DataGridSorting(_currentPage = 1);

        private void ComboBoxProductType_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            DataGridSorting(_currentPage = 1);

        private void ButtonPreviousPage_Click(object sender, RoutedEventArgs e) =>
            DataGridSorting(_currentPage - 1 > 0 ? --_currentPage : _currentPage);


        private void ButtonNextPage_Click(object sender, RoutedEventArgs e) =>
            DataGridSorting(++_currentPage);

        private void ButtonReset_Click(object sender, RoutedEventArgs e) =>
            DataGridProducts.ItemsSource = DefaultList(_currentPage = 1);
    }
}
