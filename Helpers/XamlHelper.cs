using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Coursework.Helpers
{
    public static class XamlHelper 
    {
        public static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = string.Empty;
            textBox.GotFocus -= TextBox_GotFocus;
        }
    }
}
