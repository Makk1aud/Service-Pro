﻿using Coursework.Classes;
using Coursework.DataApp;
using Coursework.Pages.Admin;
using Coursework.Repository;
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
    public partial class AdminWindow : Window
    {
        public AdminWindow(Employee employee)
        {
            InitializeComponent();
            AdminClass.RepositoryManager = new RepositoryManager(new CourseworkEntities());
            AdminClass.FrameLeftPanel = FrameLeftPanel;
            AdminClass.FrameMainStruct = FrameMainStruct;
            AdminClass.Employee = employee;
            FrameLeftPanel.Navigate(new PageLeftPanel());
            FrameMainStruct.Navigate(new PageMerchandising());
        }
    }
}
