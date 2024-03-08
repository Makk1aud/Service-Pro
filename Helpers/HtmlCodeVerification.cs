using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Coursework.Helpers
{
    public static class HtmlCodeVerification
    {
        private static string HtmlText;
        static HtmlCodeVerification()
        {
            try
            {
                var sr = new StreamReader(@"Html/EmailMessage.html");
                var line = sr.ReadLine();
                while (line != null)
                {
                    HtmlText += line;
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Произошла ошибка во время отправки кода подтверждения." +
                    "\nОбратитесь к техническому администратору.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        public static string InsertCodeIntoText(string code)
        {
            if (HtmlText is null)
                return null;
            var index = HtmlText.IndexOf("Code:") + 5;
            return HtmlText.Insert(index, code);
        }
        
    }
}
