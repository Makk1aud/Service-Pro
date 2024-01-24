using Coursework.Classes;
using Coursework.DataApp;
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
        private Product _product;
        public PageProductHandling(Product product)
        {
            InitializeComponent();
            _product = product;
            this.DataContext = new ProductHandlingViewModel(_product);

            DataGridExpenditure.ItemsSource = ExpertClass
                .RepositoryManager
                .Expenditure
                .FindByConditionGeneric(x => x.product_id == _product.product_id, trackChanges: true);
        }

        private async void ButtonChangeStatus_Click(object sender, RoutedEventArgs e) =>
           await ChangeProductStatusAsync(Convert.ToInt32((sender as Button).Tag));

        private void ButtomAddMaterial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e) =>
            ExpertClass.FrameMainStruct.Navigate(new PageMyService());

        private async Task ChangeProductStatusAsync(int statusId)
        {
            _product.pr_status_id = statusId;
            await ExpertClass.RepositoryManager.SaveAsync();
            this.DataContext = new ProductHandlingViewModel(_product);
        }

        private void TextBoxProductName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBoxProductType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonReset_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
