using Coursework.Classes;
using Coursework.DataApp;
using Coursework.Helpers;
using Coursework.ValidationsModels;
using Coursework.ValidationsModels.Extensions;
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
using System.Windows.Threading;

namespace Coursework.Pages.Admin
{
    public partial class PageAddEmploye : Page
    {
        private bool _passwordValidation = false;
        private bool _emailValidation = false;
        private int? _verificationCode;

        private DispatcherTimer _timer;
        private const int _constIntervalMs = 1000;
        private const int _timerDuration = 15;
        private DateTime _timerEndTime;
        public PageAddEmploye()
        {
            InitializeComponent();
            this.DataContext = new EmployeeValidation();

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(_constIntervalMs) };
            _timer.Tick += new EventHandler(timer_tick);
        }

        private void timer_tick(object sender, EventArgs e)
        {
            var timeNow = (_timerEndTime - DateTime.Now).Seconds;
            if (timeNow <= 0)
            {
                _timer.Stop();
                ButtonSendEmail.IsEnabled = true;
            }
        }

        private void ButtonSendEmail_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidationExtensions.EmailValidation(TextBoxEmail.Text))
            {
                TextBlockEmailValidation.Text = "Неверный формат почты";
                return;
            }

            _verificationCode = EmailHelper.SendEmailCode(TextBoxEmail.Text);

            _timer.Start();
            _timerEndTime = DateTime.Now + TimeSpan.FromSeconds(_timerDuration);

            ButtonSendEmail.IsEnabled = false;
        }

        private void TextBoxRepeatPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBoxRepeatPassword.GotFocus -= TextBoxRepeatPassword_GotFocus;
            PasswordBoxRepeatPassword.PasswordChanged += PasswordBoxRepeatPassword_PasswordChanged;
        }

        private void ButtonCheckCode_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(TextBoxEmailCode.Text, out var code)
                && code == _verificationCode)
            {
                _emailValidation = true;
                ButtonSendEmail.IsEnabled = false;
                TextBlockEmailCodeVerification.Text = string.Empty;
                TextBoxEmail.TextChanged += TextBoxEmail_TextChanged;
                TextBlockEmailCodeVerification.Foreground = Brushes.Green;
                TextBlockEmailCodeVerification.Text = "Успешно";
            }
            else
            {
                TextBlockEmailCodeVerification.Foreground = Brushes.Red;
                TextBlockEmailCodeVerification.Text = "Неверный код";
            }
        }

        private bool CheckErrors()
        {
            var enable = true;
            if (!_passwordValidation || !_emailValidation)
                return false;
            foreach(var element in StackPanelEmpInfo.Children)
            {
                if(element is TextBox field)
                    if(Validation.GetHasError(field) || String.IsNullOrEmpty(field.Text))
                        enable = false;
            }

            var busyLoginCode = AdminClass.RepositoryManager.Employee.GetEmployeeByLogin(TextBoxLoginPassword.Text, trackChanges: false);
            if(busyLoginCode != null)
            {
                TextBlockPasswordVerification.Text = "Данный код занят";
                return false;
            }

            return enable;
        }

        private async void ButtonAddEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckErrors())
            {
                TextBlockEmployeValidation.Text = "Все поля должны быть заполенены";
                return;
            }

            var emp = new Employee
            {
                firstname = TextBoxFirstname.Text,
                lastname = TextBoxLastname.Text,
                email = TextBoxEmail.Text,
                login_code = PasswordBoxRepeatPassword.Password,
                phone = TextBoxPhone.Text,
                position_id = 1
            };

            AdminClass.RepositoryManager.Employee.CreateEmployee(emp);
            await AdminClass.RepositoryManager.SaveAsync();
            AdminClass.FrameMainStruct.Navigate(new PageListOfEmployes());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.GoBack();

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            _verificationCode = null;
            _emailValidation = false;
            ButtonSendEmail.IsEnabled = true;
            TextBlockEmailCodeVerification.Text = string.Empty;
            TextBoxEmail.TextChanged -= TextBoxEmail_TextChanged;
        }

        private void PasswordBoxRepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBoxRepeatPassword.Password != TextBoxLoginPassword.Text
                || string.IsNullOrEmpty(TextBoxLoginPassword.Text))
            {
                TextBlockPasswordVerification.Text = "Пароли должны совпадать";
                _passwordValidation = false;
                return;
            }
            _passwordValidation = true;
            TextBlockPasswordVerification.Text = string.Empty;
        }
    }
}
