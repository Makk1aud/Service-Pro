using Coursework.Classes;
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
    /// <summary>
    /// Логика взаимодействия для PageLeftPanel.xaml
    /// </summary>
    public partial class PageLeftPanel : Page
    {
        public PageLeftPanel()
        {
            InitializeComponent();
        }

        private void ButtonMainPage_Click(object sender, RoutedEventArgs e)
        {
            AdminClass.FrameMainStruct.Navigate(new PageMerchandising());
        }

        private void ButtonListOfExperts_Click(object sender, RoutedEventArgs e)
        {
            AdminClass.FrameMainStruct.Navigate(new PageListOfEmployes());
        }
    }
}
