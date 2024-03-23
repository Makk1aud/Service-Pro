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

            DataGridClients.ItemsSource = AdminClass.RepositoryManager.Client.GetClients(trackChanges : true);
            ComboBoxPrType.ItemsSource = AdminClass.RepositoryManager.ProductType.FindAllGeneric(trackChanges : false);
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
            DataGridClients.ItemsSource = AdminClass
                .RepositoryManager
                .Client
                .GetClients(new ClientParameters 
                        { 
                            PhoneNum= TextBoxPhone.Text 
                        }, 
                        trackChanges : true);
        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            PageAddClient pageAddClient = new PageAddClient();
            AdminClass.FrameMainStruct.Navigate(pageAddClient);            
        }

        private async void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridClients.SelectedItem == null)
                return;
            var product = new Product()
            {
                product_name = TextBoxPrTitle.Text,
                prod_description = TextBoxPrDesc.Text,
                manager_id = AdminClass.Employee.employee_id,
                pr_type_id = Convert.ToInt32(ComboBoxPrType.SelectedValue),
                client_id = (DataGridClients.SelectedItem as Client).client_id,
                pr_status_id = 2
            };
            AdminClass.RepositoryManager.Product.CreateProduct(product);
            await AdminClass.RepositoryManager.SaveAsync();
            ResetFields();
        }

        private async void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            var client = DataGridClients.SelectedItem as Client;
            if (client is null)
                return;

            AdminClass.RepositoryManager.Client.DeleteClient(client);
            await AdminClass.RepositoryManager.SaveAsync();

            DataGridClients.ItemsSource = AdminClass
                .RepositoryManager
                .Client
                .GetClients(trackChanges : true);
        }

        private void ButtonResetSorting_Click(object sender, RoutedEventArgs e) =>
            TextBoxPhone.Text = "+";

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            var client = DataGridClients.SelectedItem as Client;
            if (client is null)
                return;

            AdminClass.FrameMainStruct.Navigate(new PageAboutClientProducts(client));
        }
        private void ResetFields()
        {
            ComboBoxPrType.SelectedItem = ComboBoxPrType.Items[0];
            TextBoxPrDesc.Text = string.Empty;
            TextBoxPrTitle.Text = string.Empty;
        }

        private void TextBoxAboutProducts_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ButtonAddProduct != null)
                ButtonAddProduct.IsEnabled = ValidationExtensions.FieldValidation((sender as TextBox).Text, min:3,max: 90);        
        }
    }
}