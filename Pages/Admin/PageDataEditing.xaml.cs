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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework.Pages.Admin
{
    public partial class PageDataEditing : Page
    {
        public PageDataEditing()
        {
            InitializeComponent();
            ManufacturerFiltering();
            MaterialTypeFiltering();
        }

        private async void ButtonAddManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(TextBoxManufacturer.Text))
                return;

            AdminClass.RepositoryManager.Manufacturer.CreateGeneric(new Manufacturer { manufacturer_title = TextBoxManufacturer.Text });
            await AdminClass.RepositoryManager.SaveAsync();
            ManufacturerFiltering();
        }
        private void ManufacturerFiltering()
        {
            var listOfManufacturers = AdminClass.RepositoryManager.Manufacturer.FindAllGeneric(trackChanges: true);
            if (!string.IsNullOrEmpty(TextBoxManufacturerFilterTitle.Text))
                listOfManufacturers = listOfManufacturers.Where(x => x.manufacturer_title.ToLower().Contains(TextBoxManufacturerFilterTitle.Text));
            DataGridManufacturers.ItemsSource = listOfManufacturers;
        }

        private void MaterialTypeFiltering()
        {
            var listOfMatTypes = AdminClass.RepositoryManager.MaterialType.FindAllGeneric(trackChanges: true);
            if (!string.IsNullOrEmpty(TextBoxMaterialFilterTypeName.Text))
                listOfMatTypes = listOfMatTypes.Where(x => x.mat_type_title.ToLower().Contains(TextBoxMaterialFilterTypeName.Text));
            DataGridMaterialTypes.ItemsSource = listOfMatTypes;
        }

        private void TextBoxManufacturerTitle_TextChanged(object sender, TextChangedEventArgs e) =>
            ManufacturerFiltering();

        private void TextBoxMaterialTypeName_TextChanged(object sender, TextChangedEventArgs e) =>
            MaterialTypeFiltering();

        private async void MenuItemDeleteManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridManufacturers.SelectedItem is Manufacturer manufacturer)
            {
                AdminClass.RepositoryManager.Manufacturer.DeleteGeneric(manufacturer);
                await AdminClass.RepositoryManager.SaveAsync();
                ManufacturerFiltering();
            }
        }

        private async void MenuItemDeleteMatType_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridMaterialTypes.SelectedItem is MaterialType materialType)
            {
                AdminClass.RepositoryManager.MaterialType.DeleteGeneric(materialType);
                await AdminClass.RepositoryManager.SaveAsync();
                MaterialTypeFiltering();
            }
        }

        private async void ButtonAddMaterialType_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxMaterialType.Text))
                return;

            AdminClass.RepositoryManager.MaterialType.CreateGeneric(new MaterialType { mat_type_title = TextBoxMaterialType.Text });
            await AdminClass.RepositoryManager.SaveAsync();
            MaterialTypeFiltering();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.GoBack();    
    }
}
