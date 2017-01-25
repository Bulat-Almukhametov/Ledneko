using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "khattartmasi@rocketmail.com";
        public string MailFromAddress = "night_bulat@mail.ru";
        public bool UseSsl = true;
        public string Username = "Night_Bulat";
        public string Password = "eqEUhae";
        public string ServerName = "smtp.mail.ru";
        public int ServerPort = 25;
        public bool WriteAsFile = false;
        public string FileLocation = @"C:\Users\w\Desktop\sports_store_emails\";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings = new EmailSettings();
        public void ProcessOrder(Solution solution, ContactDetails contactInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username,
                    emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Был сделан новый заказ")
                    .AppendLine("---");

                body.AppendFormat("Клиент: {0}, тел.{1}", contactInfo.CustomerName, contactInfo.PhoneNumber)
                    .AppendLine()
                    .AppendLine(contactInfo.Text);

                MailMessage mailMessage = new MailMessage(
                    //emailSettings.MailFromAddress, //From
                    contactInfo.EmailAddress,
                    emailSettings.MailToAddress, //To
                    "Получен новый заказ!", //Subject
                    body.ToString()); //Body

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }


        }
    }
}
