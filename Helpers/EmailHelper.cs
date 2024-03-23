using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Coursework.Helpers
{
    public static class EmailHelper
    {
        public static int? SendEmailCode(string receiverEmail)
        {
            MailAddress from = new MailAddress("serviceprotech.omsk@gmail.com", "Сервисный центр");
            var verificationCode = new Random().Next(1000, 9999);
            var htmlBody = HtmlCodeVerification.InsertCodeIntoText(verificationCode.ToString());
            if (htmlBody is null)
                return null;

            MailMessage mailMessage = new MailMessage
            {
                From = from,
                Subject = "Код подтверждения",
                Body = htmlBody,
                IsBodyHtml = true
            };
            mailMessage.To.Add(new MailAddress(receiverEmail));

            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("serviceprotech.omsk@gmail.com", "hypvanfwowgvhvzn"),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            smtp.Send(mailMessage);
            return verificationCode;
        }
    }
}
