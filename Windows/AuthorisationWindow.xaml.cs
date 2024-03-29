﻿using Coursework.Classes;
using Coursework.Pages.Authorisation;
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
using Coursework.DataApp;
using Coursework.Pages.Admin;

namespace Coursework
{
    public partial class AuthorisationWindow : Window
    {
        public AuthorisationWindow()
        {
            InitializeComponent();
            AuthorisationClass.FrameAuth = FrameAuth;
            FrameAuth.Navigate(new PageAuthorisation());
        }
    }
}
