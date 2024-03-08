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

namespace Coursework.Pages.Admin
{
    public partial class PageProductCheck : Page
    {
        private readonly Product _product;
        private readonly Client _client;
        private readonly DiscountCard _discountCard;
        private double finalPrice;
        private int? verificationCode;
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
            finalPrice = materialSum < 500 ? 500
                : ((double)materialSum * 1.2);

            if(_discountCard is null)
                TextBlockOrderSum.Text = finalPrice.ToString();
            else
            {
                var finalPriceWithDiscount = finalPrice * ((100.0 - _discountCard.discount) / 100.0);
                TextBlockOrderSum.Text = finalPriceWithDiscount.ToString();
            }            
        }

        private void ButtonSendCodeToEmail_Click(object sender, RoutedEventArgs e) =>
            SendEmailCode();

        private void SendEmailCode()
        {
            MailAddress from = new MailAddress("serviceprotech.omsk@gmail.com", "Сервисный центр");
            verificationCode = new Random().Next(1000, 9999);
            var htmlBody = HtmlCodeVerification.InsertCodeIntoText(verificationCode.ToString());
            if (htmlBody is null)
                return;

            MailMessage mailMessage = new MailMessage
            {
                From = from,
                Subject = "Код подтверждения",
                Body = htmlBody,
                IsBodyHtml = true
            };
            mailMessage.To.Add(new MailAddress(_client.email));

            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("serviceprotech.omsk@gmail.com", "hypvanfwowgvhvzn"),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            smtp.Send(mailMessage);
        }

        private async void ButtonCheckCode_Click(object sender, RoutedEventArgs e)
        {
            if (verificationCode is null)
                return;

            if (TextBoxEmailCode.Text == verificationCode.ToString())
                MessageBox.Show("Проверка пройдена!");
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

        private void ButtonPrintGuarantee_Click(object sender, RoutedEventArgs e)
        {
            var wordHelper = new WordHelper("Word/WordTempleGuarantee.docx");

            var replacementKeys = new Dictionary<string, string>
            {
                {"<ProductCode>", _product.pr_status_id.ToString() },
                {"<ProductName>", _product.product_name },
                {"<ProductPrice>", finalPrice.ToString()},
                {"<Discount>", _discountCard.discount.ToString() },
                {"<ProductPriceFinaly>", TextBlockOrderSum.Text }
            };

            wordHelper.WriteIntoDocument(replacementKeys, "Guarantees", _product.product_name);
        }
    }
}
