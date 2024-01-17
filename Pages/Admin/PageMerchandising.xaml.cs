using Coursework.Classes;
using Coursework.DataApp;

using Coursework.Repository.Extensions.FilterParameters;
using Coursework.ValidationsModels.Extensions;
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
        public PageMerchandising()
        {
            InitializeComponent();

            DataGridClients.ItemsSource = AdminClass.repositoryManager.Client.GetClients(trackChanges : true);
            ComboBoxPrType.ItemsSource = AdminClass.repositoryManager.ProductType.FindAllGeneric(trackChanges : false);
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
            //if (TextBoxPhone.Text != string.Empty)
            //    DataGridClients.ItemsSource = _clientRepository.GetClientsByPhone(TextBoxPhone.Text);
            //else
            //    DataGridClients.ItemsSource = _clientRepository.GetClients();
            DataGridClients.ItemsSource = AdminClass
                .repositoryManager
                .Client
                .GetClients(new ClientParameters 
                        { 
                            PhoneNum= TextBoxPhone.Text 
                        }, 
                        trackChanges : false);
        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            PageAddClient pageAddClient = new PageAddClient();
            AdminClass.frameMainStruct.Navigate(pageAddClient);            
        }

        private async void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridClients.SelectedItem == null)
                return;
            var product = new Product()
            {
                product_name = TextBoxPrTitle.Text,
                prod_description = TextBoxPrDesc.Text,
                manager_id = AdminClass.employee.employee_id,
                pr_type_id = Convert.ToInt32(ComboBoxPrType.SelectedValue),
                client_id = (DataGridClients.SelectedItem as Client).client_id,
                pr_status_id = 1
            };
            AdminClass.repositoryManager.Product.CreateProduct(product);
            await AdminClass.repositoryManager.SaveAsync();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e) =>
            ButtonAddProduct.IsEnabled = ValidationExtensions.ProductNameValidation((sender as TextBox).Text);

        private async void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            var client = DataGridClients.SelectedItem as Client;
            AdminClass.repositoryManager.Client.DeleteClient(client);
            await AdminClass.repositoryManager.SaveAsync();

            DataGridClients.ItemsSource = AdminClass
                .repositoryManager
                .Client
                .GetClients(trackChanges : false);
        }
    }
}