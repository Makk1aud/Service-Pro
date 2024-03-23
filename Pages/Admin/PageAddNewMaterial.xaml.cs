using Coursework.Classes;
using Coursework.DataApp;
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
    public partial class PageAddNewMaterial : Page
    {
        private Material material = new Material();
        public PageAddNewMaterial()
        {
            InitializeComponent();
            DataGridMaterialTypesSorting();
            DataGridManufacturerSorting();
        }

        private void DataGridManufacturerSorting()
        {
            var listOfManufacturers = AdminClass.RepositoryManager.Manufacturer.FindAllGeneric(trackChanges: true);

            if (!string.IsNullOrEmpty(TextBoxManufacturerTitle.Text))
                listOfManufacturers = listOfManufacturers
                    .Where(x => x.manufacturer_title.ToLower().Contains(TextBoxManufacturerTitle.Text.ToLower()));
            DataGridManufacturers.ItemsSource = listOfManufacturers;
        }

        private void DataGridMaterialTypesSorting()
        {
            var listOfMaterialsTypes = AdminClass.RepositoryManager.MaterialType.FindAllGeneric(trackChanges: true);

            if (!string.IsNullOrEmpty(TextBoxManufacturerTitle.Text))
                listOfMaterialsTypes = listOfMaterialsTypes
                    .Where(x => x.mat_type_title.ToLower().Contains(TextBoxMaterialTypeName.Text.ToLower()));
            DataGridMaterialTypes.ItemsSource = listOfMaterialsTypes;
        }

        private void TextBoxMaterialTypeName_TextChanged(object sender, TextChangedEventArgs e) =>
            DataGridMaterialTypesSorting();


        private void DataGridMaterialTypes_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var materialType = DataGridMaterialTypes.SelectedItem as MaterialType;
            if (materialType is null)
                return;

            material.mat_type_id = materialType.mat_type_id;
            material.MaterialType = materialType;
            UpdateDataContext();
        }

        private void DataGridManufacturers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var manufacturer = DataGridManufacturers.SelectedItem as Manufacturer;
            if (manufacturer is null)
                return;

            material.manufacturer_id = manufacturer.manufacturer_id;
            material.Manufacturer = manufacturer;
            UpdateDataContext();
        }

        private void UpdateDataContext()
        {
            this.DataContext = null;
            this.DataContext = material;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.Navigate(new PageSupplyMaterials());


        private void TextBoxManufacturerTitle_TextChanged(object sender, TextChangedEventArgs e) =>
            DataGridManufacturerSorting();

        private void TextBoxMaterialTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            material.material_title = TextBoxMaterialTitle.Text;
            UpdateDataContext();
        }

        private void TextBoxMaterialPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            material.material_price = int.TryParse(TextBoxMaterialPrice.Text, out var price)
                ? price 
                : 0;
            UpdateDataContext();
        }

        private async void ButtonAddNewMaterial_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxMaterialPrice.Text) ||
                string.IsNullOrEmpty(TextBoxMaterialTitle.Text))
                return;
            AdminClass.RepositoryManager.Material.CreateMaterial(material);
            await AdminClass.RepositoryManager.SaveAsync();
        }
    }
}
