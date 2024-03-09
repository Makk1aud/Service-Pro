using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Coursework.Classes;
using Coursework.DataApp;
using Coursework.Helpers;
using Coursework.ValidationsModels;
using Coursework.ValidationsModels.Extensions;


namespace Coursework.Pages.Admin
{

    public partial class PageAddClient : Page
    {
        private ClientValidation _client;
        private bool _emailValidation = false;
        private int? _verificationCode;        

        private DispatcherTimer _timer;
        private const int _constIntervalMs = 1000;
        private int _timerTime = 0;
        private int _timerDuration = 15;

        public PageAddClient()
        {
            InitializeComponent();
            _client = new ClientValidation();
            this.DataContext = _client;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(_constIntervalMs) };
            _timer.Tick += new EventHandler(timer_tick);
        }

        private async void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckErrors())
                return;
            Client newClient = new Client()
            {
                firstname = _client.FirstName,
                lastname = _client.LastName,
                phone = _client.PhoneNum,
                email = _client.Email
            };

            AdminClass.RepositoryManager.Client.CreateClient(newClient);
            await AdminClass.RepositoryManager.SaveAsync();
            AdminClass.FrameMainStruct.Navigate(new PageMerchandising());
        }

        private bool CheckErrors()
        {
            bool enable = true;
            foreach (var element in StackPanelElemets.Children)
            {
                if (element is TextBox textBox)
                {
                    if (Validation.GetHasError(textBox) || string.IsNullOrEmpty(textBox.Text))
                        enable = false;
                }
            }

            if (!string.IsNullOrEmpty(TextBoxEmail.Text))
                enable = _emailValidation;
            return enable;
        }

        private void ButtonCheckCode_Click(object sender, RoutedEventArgs e)
        {
            if (_verificationCode is null ||
                TextBoxEmailCode.Text != _verificationCode.ToString())
            {
                TextBlockCodeValidationMessage.Text = "Неверный код";
                return;
            }

            _emailValidation = true;
            MessageBox.Show("Проверка пройдена!",
                    "Результат",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

            TextBlockCodeValidationMessage.Text = null;
            ButtonSendCode.IsEnabled = false;
        }

        private void ButtonSendCode_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidationExtensions.EmailValidation(TextBoxEmail.Text))
            {
                TextBlockEmailValidationMessage.Text = "Неверный формат почты";
                return;
            }            

            _verificationCode = EmailHelper.SendEmailCode(TextBoxEmail.Text);
            _timer.Start();
            ButtonSendCode.IsEnabled = false;
            TextBoxEmail.TextChanged += TextBoxEmail_TextChanged;
        }

        private void timer_tick(object sender, EventArgs e)
        {
            TextBlockTimerToSend.Text = $"Время до повторной отправки {30 - _timerTime}";
            _timerTime++;
            if (_timerTime >= _timerDuration)
            {
                _timer.Stop();
                ButtonSendCode.IsEnabled = true;
                TextBlockTimerToSend.Text = null;
            }
            var now = DateTime.Now.Millisecond;
            _timer.Interval = TimeSpan.FromMilliseconds(1000 - now);
        }

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            _emailValidation = false;
            ButtonSendCode.IsEnabled = true;
            TextBoxEmail.TextChanged -= TextBoxEmail_TextChanged;
        }
    }
}
