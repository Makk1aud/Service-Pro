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
using System.Windows.Shapes;

namespace Coursework
{
    /// <summary>
    /// Логика взаимодействия для ExpertWindow.xaml
    /// </summary>
    public partial class ExpertWindow : Window
    {
        public ExpertWindow(Employee employee)
        {
            InitializeComponent();
            ExpertClass.employee = employee;
        }
    }
}
