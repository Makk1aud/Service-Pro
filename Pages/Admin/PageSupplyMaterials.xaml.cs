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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework.Pages.Admin
{
    public partial class PageSupplyMaterials : Page
    {
        public PageSupplyMaterials()
        {
            InitializeComponent();

            ComboBoxMaterialType.ItemsSource = AdminClass
                .RepositoryManager
                .MaterialType
                .FindAllGeneric(trackChanges: false);

            DataGridMaterialsSorting();
        }

        private void DataGridMaterialsSorting()
        {
            if (TextBoxMaxPrice is null 
                ||TextBoxMinPrice is null
                || DataGridMaterials is null)
                return;

            DataGridMaterials.ItemsSource = AdminClass
                .RepositoryManager
                .Material
                .GetMaterials(new MaterialParameters
                {
                    MaterialTitle = TextBoxProductName.Text,
                    MaterialTypeId = Convert.ToInt32(ComboBoxMaterialType.SelectedValue),
                    MaxPrice = int.TryParse(TextBoxMaxPrice.Text, out var maxPrice) ? maxPrice : 99000,
                    MinPrice = int.TryParse(TextBoxMinPrice.Text, out var minPrice) ? minPrice : 0,
                }, trackChanges: true);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.Navigate(new PageMerchandising());

        private void ButtonResetSorting_Click(object sender, RoutedEventArgs e)
        {
            TextBoxMaxPrice.Text = string.Empty;
            TextBoxMinPrice.Text = string.Empty;
            TextBoxProductName.Text = string.Empty;
            ComboBoxMaterialType.SelectedItem = null;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) =>
            DataGridMaterialsSorting();

        private void ComboBoxProductType_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            DataGridMaterialsSorting();

        private async Task AddQuantityToMaterial(Material material)
        {
            var quantity = int.TryParse(TextBoxAddMaterials.Text, out var addQuantity) 
                ? addQuantity : 0;
            if (quantity <= 0)
                return;

            material.quantity += quantity;
            await AdminClass.RepositoryManager.SaveAsync();
        }

        private async void ButtonAddMaterials_Click(object sender, RoutedEventArgs e)
        {
            var material = DataGridMaterials.SelectedItem as Material;
            if(material is null)
                return;

            await AddQuantityToMaterial(material);
            DataGridMaterialsSorting();
            this.DataContext = null;
            this.DataContext = material;
        }

        private void ButtonAddAndPrintMaterials_Click(object sender, RoutedEventArgs e)
        {
            //Здесь надо сделать печать документа о поступлении
        }

        private void ButtonAddNewMaterials_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.Navigate(new PageAddNewMaterial());

        private void DataGridMaterials_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) =>
            this.DataContext = DataGridMaterials.SelectedItem as Material;
    }
}
