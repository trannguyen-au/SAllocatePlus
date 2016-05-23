using SwinSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SwinSchool.BusinessLogicServer
{
    public class EmailService
    {
        public void SendUpdatedPasswordEmail(MyUser user)
        {
            var from = ConfigurationManager.AppSettings["AdminEmail"];
            var to = user.Email;
            var subject = "Your password has been updated";
            var body = "Dear {0}, \n\n" +
                "Your account password has been reset to: {1} \n\n" +
                "Kind regards, \n" +
                "Swinburne Enterprise Project Lab";
            body = string.Format(body, user.Name, user.Password);
            SendMail(from, to, subject, body);
        }

        private void SendMail(string from, string to, string subject, string body)
        {
            MailMessage mail = new MailMessage(from, to);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.EnableSsl = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Timeout = 10000;
            client.Host = "mail.wnext.net.au";
            client.Credentials = new NetworkCredential("contact@wnext.net.au", "Pentium4");

            mail.Subject = subject;
            mail.Body = body;

            client.Send(mail);
        }
    }
}
