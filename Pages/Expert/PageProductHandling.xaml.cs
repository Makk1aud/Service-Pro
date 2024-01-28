using Coursework.Classes;
using Coursework.DataApp;
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
    public partial class PageProductHandling : Page
    {
        private readonly Product _product;
        public PageProductHandling(Product product)
        {
            InitializeComponent();
            _product = product;
            this.DataContext = new ProductHandlingViewModel(_product);

            DataGridExpenditureFilling();
            DataGridMaterialsSorting();
            ComboBoxProductType.ItemsSource = ExpertClass
                .RepositoryManager
                .MaterialType
                .FindAllGeneric(trackChanges: false);
        }

        private async void ButtonChangeStatus_Click(object sender, RoutedEventArgs e) =>
           await ChangeProductStatusAsync(Convert.ToInt32((sender as Button)?.Tag));

        private void ButtonBack_Click(object sender, RoutedEventArgs e) =>
            ExpertClass.FrameMainStruct.Navigate(new PageMyService());

        private async Task ChangeProductStatusAsync(int statusId)
        {
            _product.pr_status_id = statusId;
            await ExpertClass.RepositoryManager.SaveAsync();
            this.DataContext = new ProductHandlingViewModel(_product);
        }

        private void DataGridExpenditureFilling() =>
            DataGridExpenditure.ItemsSource = ExpertClass
                .RepositoryManager
                .Expenditure
                .FindByConditionGeneric(x => x.product_id == _product.product_id,
                trackChanges: true);

        private void DataGridMaterialsSorting()
        {
            if (TextBoxMaxPrice is null ||
                TextBoxMinPrice is null)
                return;

            DataGridMaterials.ItemsSource = ExpertClass
                .RepositoryManager
                .Material
                .GetMaterials(new MaterialParameters
                {
                    MaterialTitle = TextBoxProductName.Text,
                    MaterialTypeId = Convert.ToInt32(ComboBoxProductType.SelectedValue),
                    MaxPrice = int.TryParse(TextBoxMaxPrice.Text, out var maxPrice) ? maxPrice : 99000,
                    MinPrice = int.TryParse(TextBoxMinPrice.Text, out var minPrice) ? minPrice : 0,
                }, trackChanges: true);
        }


        private async void ButtonAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            var material = DataGridMaterials.SelectedItem as Material;
            var expMat = ExpertClass
                .RepositoryManager
                .Expenditure
                .FindByConditionGeneric(x => x.material_id == material.material_id
                && x.product_id == _product.product_id, trackChanges: true)
                .FirstOrDefault();
            if (expMat != null)            
                expMat.quantity += 1;            
            else
                ExpertClass.RepositoryManager.Expenditure.CreateGeneric(new Expenditure
                {
                    material_id = material.material_id,
                    product_id = _product.product_id,
                    quantity = 1,
                    capture_date = DateTime.Now
                });

            await ExpertClass.RepositoryManager.SaveAsync();
            DataGridExpenditureFilling();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) =>
            DataGridMaterialsSorting();

        private void ComboBoxProductType_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            DataGridMaterialsSorting();

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            TextBoxProductName.Text = string.Empty;
            TextBoxMinPrice.Text = "0";
            TextBoxMaxPrice.Text = "99000";
            ComboBoxProductType.SelectedValue = null;
            DataGridMaterialsSorting();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) =>
            TextBoxProductName.Focus();

        private async void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            var expMaterial = DataGridExpenditure.SelectedItem as Expenditure;

            if (expMaterial.quantity > 1)
                expMaterial.quantity--;
            else
                ExpertClass
                    .RepositoryManager
                    .Expenditure.DeleteGeneric(expMaterial);

            await ExpertClass.RepositoryManager.SaveAsync();
            DataGridExpenditureFilling();
        }

        private async void MenuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            var expMaterial = DataGridExpenditure.SelectedItem as Expenditure;
            expMaterial.quantity++;

            await ExpertClass.RepositoryManager.SaveAsync();
            DataGridExpenditureFilling();
        }
    }
}
