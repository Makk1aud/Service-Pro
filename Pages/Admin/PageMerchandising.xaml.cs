using Coursework.Classes;
using Coursework.DataApp;
using Coursework.Interfaces;
using Coursework.Models;
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
using Xceed.Wpf.Toolkit;

namespace Coursework.Pages.Admin
{
    public partial class PageMerchandising : Page
    {
        private IClientRepository _clientRepository;
        private IProductRepository _productRepository;
        public PageMerchandising()
        {
            InitializeComponent();
            _clientRepository = new ClientRepository();
            _productRepository = new ProductRepository();

            DataGridClients.ItemsSource = _clientRepository.GetClients();
            ComboBoxPrType.ItemsSource = _productRepository.GetProductTypes();
            ComboBoxPrType.SelectedItem = ComboBoxPrType.Items[0];
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.Text = string.Empty;
            //XamlHelper.TextBox_GotFocus(textBox, e);
            if (textBox.Equals(TextBoxPhone))
            {
                textBox.TextChanged += TextBoxPhone_TextChanged;
                textBox.Text = "+";
            }
            textBox.GotFocus -= TextBox_GotFocus;
        }

        private void TextBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxPhone.Text != string.Empty)
                DataGridClients.ItemsSource = _clientRepository.GetClientsByPhone(TextBoxPhone.Text);
            else
                DataGridClients.ItemsSource = _clientRepository.GetClients();
        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            PageAddClient pageAddClient = new PageAddClient(_clientRepository);
            AdminClass.frameMainStruct.Navigate(pageAddClient);
            
        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridClients.SelectedItem == null)
                return;
            var product = new Product()
            {
                product_name = TextBoxPrTitle.Text,
                prod_description = TextBoxPrDesc.Text,
                pr_type_id = Convert.ToInt32(ComboBoxPrType.SelectedValue),
                client_id = (DataGridClients.SelectedItem as Client).client_id,
                pr_status_id = 1
            };
            _productRepository.AddProduct(product);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            ButtonAddProduct.IsEnabled = !String.IsNullOrEmpty(textBox.Text);
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            var client = DataGridClients.SelectedItem as Client;
            _clientRepository.DeleteClient(client);
            DataGridClients.ItemsSource = _clientRepository.GetClients();
        }
    }
}