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
    public partial class PageAboutClientProducts : Page
    {
        private readonly Client _client;
        public PageAboutClientProducts(Client client)
        {
            InitializeComponent();
            _client = client;
            this.DataContext = _client;
            DataGridProductsSorting();
        }

        private void DataGridProductsSorting() =>
            DataGridProducts.ItemsSource = AdminClass
            .RepositoryManager
            .Product
            .GetProducts(new ProductParameters
            {
                ClientId = _client.client_id,
                SearchName = TextBoxProductName.Text,
                ProductTypeId = Convert.ToInt32(ComboBoxProductType.SelectedValue)
            }, trackChanges: true);

        private void ButtonBack_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.GoBack();

        private void TextBoxProductName_TextChanged(object sender, TextChangedEventArgs e) =>
            DataGridProductsSorting();

        private void ComboBoxProductType_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            DataGridProductsSorting();

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            TextBoxProductName.Text = string.Empty;
            ComboBoxProductType.SelectedItem = null;
        }

        private void ButtonSelectProduct_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
