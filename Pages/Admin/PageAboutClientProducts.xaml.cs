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
        private DiscountCard _discountCard;
        public PageAboutClientProducts(Client client)
        {
            InitializeComponent();
            _client = client;
            this.DataContext = _client;
            DataGridProductsSorting();

            ComboBoxProductType.ItemsSource = AdminClass
                .RepositoryManager
                .ProductType
                .FindAllGeneric(trackChanges: true);

            ComboBoxProductStatus.ItemsSource = AdminClass
                .RepositoryManager
                .ProductStatus
                .FindAllGeneric(trackChanges: true);

            _discountCard = AdminClass
                .RepositoryManager
                .DiscountCard
                .FindByConditionGeneric(x => x.client_id == _client.client_id, trackChanges: true)
                .FirstOrDefault();

            FillingDiscount(_discountCard);
        }

        //Проверить работу 
        private void FillingDiscount(DiscountCard discountCard)
        {
            if (discountCard is null)
                return;
            //Проверка на 0 потому что у клиента нет товаров умножение на 0 скидка ноль значит ошибка при сохранении
            ButtonIssueDiscountCard.IsEnabled = false;
            discountCard.discount = DataGridProducts.Items.Count * 5 > 30 
                ? 30 
                : DataGridProducts.Items.Count * 2 + 1;
            TextBlockDiscount.Text = $"{discountCard.discount}%";
            _discountCard = discountCard;
        }

        private void DataGridProductsSorting() =>
            DataGridProducts.ItemsSource = AdminClass
            .RepositoryManager
            .Product
            .GetClientProducts(new ProductParameters
            {
                ClientId = _client.client_id,
                SearchName = TextBoxProductName.Text,
                ProductTypeId = Convert.ToInt32(ComboBoxProductType.SelectedValue),
                ProductStatusId = Convert.ToInt32(ComboBoxProductStatus.SelectedValue)
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
            ComboBoxProductStatus.SelectedItem = null;
        }

        private void ButtonSelectProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as Product;
            if(product.pr_status_id == 3)
                AdminClass.FrameMainStruct.Navigate(new PageProductCheck(product, _client, _discountCard));
        }

        private async void ButtonIssueDiscountCard_Click(object sender, RoutedEventArgs e)
        {
            var discountCard = new DiscountCard
            {
                card_num = new Random().Next(100000, 999999).ToString(),
                client_id = _client.client_id,
                discount = 1
            };
            AdminClass.RepositoryManager.DiscountCard.CreateGeneric(discountCard);
            await AdminClass.RepositoryManager.SaveAsync();
            FillingDiscount(discountCard);
        }
    }
}
