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
        private MetaData _metaData;
        public PageListProducts()
        {
            InitializeComponent();         
            DataGridSorting(1);
            ComboBoxProductType.ItemsSource = ExpertClass
                .RepositoryManager
                .ProductType
                .FindAllGeneric(trackChanges: false);
        }

        private async void ButtonSelectProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as Product;
            product.expert_id = ExpertClass.Employee.employee_id;
            
            await ExpertClass.RepositoryManager.SaveAsync();
            DataGridSorting(_metaData.CurrentPage);
        }

        private void DataGridSorting(int pageNum)
        {
            var listOfProducts = ExpertClass
                .RepositoryManager
                .Product
                .GetProducts (new ProductParameters()
                {
                    PageNumber = pageNum,
                    SearchName = TextBoxProductName.Text,
                    SearchDesc = TextBoxProductDesc.Text,
                    ProductTypeId = Convert.ToInt32(ComboBoxProductType.SelectedValue)
                }, trackChanges: true);
            _metaData = listOfProducts.MetaData;
            this.DataContext = _metaData;
            DataGridProducts.ItemsSource = listOfProducts;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) =>
            DataGridSorting(_metaData.CurrentPage = 1);

        private void ComboBoxProductType_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            DataGridSorting(_metaData.CurrentPage = 1);

        private void ButtonPreviousPage_Click(object sender, RoutedEventArgs e) =>
            DataGridSorting(_metaData.HasPrevious ? --_metaData.CurrentPage : _metaData.CurrentPage);


        private void ButtonNextPage_Click(object sender, RoutedEventArgs e) =>
            DataGridSorting(_metaData.HasNext ? ++_metaData.CurrentPage : _metaData.CurrentPage);

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            TextBoxProductDesc.Text = string.Empty;
            TextBoxProductName.Text = string.Empty;
            ComboBoxProductType.SelectedItem = null;
            DataGridSorting(1);
        }
    }
}
