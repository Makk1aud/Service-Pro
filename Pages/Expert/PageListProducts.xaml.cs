using Coursework.Classes;
using Coursework.DataApp;
using Coursework.Pages.Expert;
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

namespace Coursework.Pages.General
{
    public partial class PageListProducts : Page
    {
        public PageListProducts()
        {
            InitializeComponent();
            DataGridProducts.ItemsSource = null;
            DataGridProducts.ItemsSource = ExpertClass
                .RepositoryManager
                .Product
                .GetProducts(trackChanges: true);
        }

        private async void ButtonSelectProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as Product;
            product.expert_id = ExpertClass.Employee.employee_id;
            await ExpertClass.RepositoryManager.SaveAsync();
        }
    }
}
