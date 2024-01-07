using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework.Helpers
{
    public static class ChangeWindows
    {
        public static void Change(Window window, Window newWindow)
        {
            newWindow.Show();
            window.Owner = newWindow;
            window.Close();
        }
    }
}
