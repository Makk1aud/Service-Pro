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

namespace Coursework.Pages.Admin
{
    public partial class PageProductCheck : Page
    {
        private readonly Product _product;
        private readonly Client _client;
        public PageProductCheck(Product product, Client client)  
        {
            InitializeComponent();
            _product = product;
            _client = client;

            this.DataContext = _product;

            var listOfExpenditure = AdminClass
                .RepositoryManager
                .Expenditure
                .FindByConditionGeneric(x => x.product_id == _product.product_id, trackChanges: true);
            DataGridMaterials.ItemsSource = listOfExpenditure;

            TextBlockOrderSum.Text = listOfExpenditure.Sum(x => x.Material.material_price * x.quantity).ToString();
        }

        private void ButtonSendCodeToEmail_Click(object sender, RoutedEventArgs e)
        {
            MailAddress from = new MailAddress("serviceprotech.omsk@gmail.com", "Сервисный центр");

            //MailAddress to = new MailAddress("1roman080283@mail.ru");

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("serviceprotech.omsk@gmail.com", "Сервисный центр"),
                Subject = "Код подтверждения",
                Body = "<h2>Ваш код подтверждения</h2>",
                IsBodyHtml = true
            };
            mailMessage.To.Add("1roman080283@mail.ru");
            
            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("serviceprotech.omsk", "Qwerty123."),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            smtp.Send(mailMessage);
        }

        private async void ButtonCheckCode_Click(object sender, RoutedEventArgs e)
        {            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e) =>
            AdminClass.FrameMainStruct.GoBack();

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) =>
            ButtonIssueOrder.Focus();

        private async void ButtonIssueOrder_Click(object sender, RoutedEventArgs e)
        {
            _product.pr_status_id = 4;
            await AdminClass.RepositoryManager.SaveAsync();
            AdminClass.FrameMainStruct.Navigate(new PageAboutClientProducts(_client));
        }
    }
}
