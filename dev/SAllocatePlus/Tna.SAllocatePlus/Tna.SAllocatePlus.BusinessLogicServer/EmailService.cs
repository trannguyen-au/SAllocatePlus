using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Tna.SAllocatePlus.BusinessLogicServer
{
    public class EmailService
    {
        public void SendMail(string from, string[] to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            foreach (var t in to)
            {
                mail.To.Add(new MailAddress(t));
            }

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.EnableSsl = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Timeout = 10000;
            client.Host = "mail.wnext.net.au";
            client.Credentials = new NetworkCredential("contact@wnext.net.au", "*******");

            mail.Subject = subject;
            mail.Body = body;

            client.Send(mail);
        }
    }
}