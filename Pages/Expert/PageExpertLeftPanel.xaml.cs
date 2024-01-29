using Coursework.Classes;
using Coursework.Pages.General;
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
    public partial class PageExpertLeftPanel : Page
    {
        public PageExpertLeftPanel()
        {
            InitializeComponent();
        }

        private void ButtonListOfProducts_Click(object sender, RoutedEventArgs e) =>
            ExpertClass.FrameMainStruct.Navigate(new PageListProducts());

        private void ButtonPersonalKabinet_Click(object sender, RoutedEventArgs e) =>
            ExpertClass.FrameMainStruct.Navigate(new PageMyService());
    }
}
