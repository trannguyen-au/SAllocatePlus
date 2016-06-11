using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Tna.SAllocatePlus.BusinessLogicServer
{
    public class EmailService
    {
        public void SendMail(string from, string to, string subject, string body)
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