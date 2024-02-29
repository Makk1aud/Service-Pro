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
    public partial class PageIssueOrder : Page
    {
        private MetaData _metaData;
        public PageIssueOrder()
        {
            InitializeComponent();
            DataGridClientsSorting(1);
        }

        public void DataGridClientsSorting(int pageNum)
        {
            var listOfClients = AdminClass
                .RepositoryManager
                .Client
                .GetClients(new ClientParameters
                {
                    PageNumber = pageNum,
                    Name = TextBoxFirstname.Text,
                    LastName = TextBoxLastName.Text,
                    PhoneNum = TextBoxPhone.Text
                }, trackChanges: false);

            _metaData = listOfClients.MetaData;
            this.DataContext = _metaData;
            DataGridClients.ItemsSource = listOfClients;
        }
        private void TextBoxLastName_TextChanged(object sender, TextChangedEventArgs e) =>
            DataGridClientsSorting(_metaData.CurrentPage = 1);

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFirstname.Text = String.Empty;
            TextBoxLastName.Text = String.Empty;
            TextBoxPhone.Text = String.Empty;
        }

        private void TextBoxPhone_GotFocus(object sender, RoutedEventArgs e) =>
            (sender as TextBox).Text = "+";

        private void ButtonSelectClient_Click(object sender, RoutedEventArgs e)
        {
            var client = (sender as Button).DataContext as Client;
            AdminClass.FrameMainStruct.Navigate(new PageAboutClientProducts(client));
        }

        private void ButtonPreviousPage_Click(object sender, RoutedEventArgs e) =>
            DataGridClientsSorting(_metaData.HasPrevious ? --_metaData.CurrentPage : _metaData.CurrentPage); 

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e) =>
            DataGridClientsSorting(_metaData.HasNext ? ++_metaData.CurrentPage : _metaData.CurrentPage);

    }
}
