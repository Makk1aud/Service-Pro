using Coursework.Classes;
using Coursework.DataApp;
using Coursework.Repository.Extensions.FilterParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using Microsoft.CodeAnalysis;
using Coursework.Helpers;
using System.Windows.Threading;
using System.Threading;

namespace Coursework.Pages.Admin
{
    public partial class PageProductCheck : Page
    {
        private readonly Product _product;
        private readonly Client _client;
        private readonly DiscountCard _discountCard;
        private double _finalPrice;
        private int? _verificationCode;

        private DispatcherTimer _timer;
        private int _timerTime = 0;
        private const int _constIntervalMs = 1000;
        private const int _timerDuration = 30;

        public PageProductCheck(Product product, Client client, DiscountCard discountCard)  
        {
            InitializeComponent();
            _product = product;
            _client = client;
            _discountCard = discountCard;

            this.DataContext = _product;

            var listOfExpenditure = AdminClass
                .RepositoryManager
                .Expenditure
                .FindByConditionGeneric(x => x.product_id == _product.product_id, trackChanges: true);
            DataGridMaterials.ItemsSource = listOfExpenditure;

            var materialSum = listOfExpenditure.Sum(x => x.Material.material_price * x.quantity);
            _finalPrice = materialSum < 500 ? 500
                : ((double)materialSum * 1.2);

            if(_discountCard is null)
                TextBlockOrderSum.Text = _finalPrice.ToString();
            else
            {
                var finalPriceWithDiscount = _finalPrice * ((100.0 - _discountCard.discount) / 100.0);
                TextBlockOrderSum.Text = finalPriceWithDiscount.ToString();
            }

            ButtonSendCodeToEmail.IsEnabled = _client.email != null;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(_constIntervalMs) };
            _timer.Tick += new EventHandler(timer_tick);
        }

        private void ButtonSendCodeToEmail_Click(object sender, RoutedEventArgs e)
        {
            _verificationCode = EmailHelper.SendEmailCode(_client.email);
            _timer.Start();
            ButtonSendCodeToEmail.IsEnabled = false;
        }            
        
        private void timer_tick(object sender, EventArgs e)
        {
            TextBlockTimerToSend.Text = $"Время до повторной отправки {_timerDuration - _timerTime}";
            _timerTime++;
            if(_timerTime >= _timerDuration)
            {
                _timer.Stop();
                ButtonSendCodeToEmail.IsEnabled = true;
                TextBlockTimerToSend.Text = null;
                _timerTime = 0;
            }
            var now = DateTime.Now.Millisecond;
            _timer.Interval = TimeSpan.FromMilliseconds(1000 - now);
        }

        private void ButtonCheckCode_Click(object sender, RoutedEventArgs e)
        {
            if (_verificationCode is null ||
                TextBoxEmailCode.Text != _verificationCode.ToString())
            {
                TextBlockValidationMessage.Text = "Неверный код";
                return;
            }

            ButtonIssueOrder.IsEnabled = true;
            MessageBox.Show("Проверка пройдена!",
                    "Результат",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

            TextBlockValidationMessage.Text = null;
            ButtonSendCodeToEmail.IsEnabled= false;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.GoBack();

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) =>
            ButtonIssueOrder.Focus();

        private async void ButtonIssueOrder_Click(object sender, RoutedEventArgs e)
        {
            if (!PrintGuarantee())
            {
                var mesBoxResult = MessageBox.Show("Хотите выдать товар без гарантии?",
                "Гарантия",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

                if (mesBoxResult == MessageBoxResult.No)
                    return;
            }
           
            _product.pr_status_id = 4;
            await AdminClass.RepositoryManager.SaveAsync();
            AdminClass.FrameMainStruct.Navigate(new PageAboutClientProducts(_client));
        }

        private bool PrintGuarantee()
        {
            var wordHelper = new WordHelper("Word/WordTempleGuarantee.docx");

            var replacementKeys = new Dictionary<string, string>
            {
                {"<ProductCode>", _product.product_id.ToString() },
                {"<ProductName>", _product.product_name },
                {"<ProductPrice>", _finalPrice.ToString()},
                {"<Discount>", _discountCard.discount.ToString() },
                {"<ProductPriceFinaly>", TextBlockOrderSum.Text }
            };

            return wordHelper.WriteIntoDocument(replacementKeys, "Guarantees", _product.product_name);
        }

        private void ButtonPassportCheck_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxBody = $"Имя: {_client.firstname}\n" +
                $"Фамилия: {_client.lastname}\n" +
                $"Номер телефона: {_client.phone}\n" +
                $"Если данные верны нажмите да";

            var mesBoxResult = MessageBox.Show(messageBoxBody,
                "Проверка данных",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if(mesBoxResult == MessageBoxResult.Yes)
                ButtonIssueOrder.IsEnabled = true;
        }
    }
}
