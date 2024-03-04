using Coursework.Classes;
using Coursework.Helpers;
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
    public partial class PageLeftPanel : Page
    {
        public PageLeftPanel()
        {
            InitializeComponent();
        }

        private void ButtonMainPage_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.Navigate(new PageMerchandising());

        private void ButtonListOfExperts_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.Navigate(new PageListOfEmployes());

        private void ButtonIssueOrder_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.Navigate(new PageIssueOrder());

        private void ButtonSignOut_Click(object sender, RoutedEventArgs e) =>
            ChangeWindows.Change(Window.GetWindow(this), new AuthorisationWindow());

        private void ButtonSupplyMaterials_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.Navigate(new PageSupplyMaterials());
    }
}
