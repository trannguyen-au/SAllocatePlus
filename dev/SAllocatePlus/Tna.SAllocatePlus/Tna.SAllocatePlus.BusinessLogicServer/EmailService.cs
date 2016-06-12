using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Tna.SAllocatePlus.BusinessLogicServer
{
    public class EmailService
    {
        private bool _isEnabled;
        private int _smtpPort;
        private string _smtpHost;
        private string _smtpAccount, _smtpPassword;

        public EmailService()
        {
            _isEnabled = ConfigurationManager.AppSettings["SmtpMailEnabled"] == "true";
            _smtpPort = string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SmtpMailPort"]) ? 0 : int.Parse(ConfigurationManager.AppSettings["SmtpMailPort"]);
            _smtpHost = ConfigurationManager.AppSettings["SmtpMailHost"];
            _smtpAccount = ConfigurationManager.AppSettings["SmtpMailAccount"];
            _smtpPassword = ConfigurationManager.AppSettings["SmtpMailPassword"];
        }

        public void SendMail(string from, string to, string subject, string body)
        {
            if (!_isEnabled) throw new Exception("Smtp email is not enabled");

            MailMessage mail = new MailMessage(from, to);

            SmtpClient client = new SmtpClient();
            client.Port = _smtpPort;
            client.EnableSsl = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Timeout = 10000;
            client.Host = _smtpHost;
            client.Credentials = new NetworkCredential(_smtpAccount, _smtpPassword);

            mail.Subject = subject;
            mail.Body = body;

            client.Send(mail);
        }
    }
}