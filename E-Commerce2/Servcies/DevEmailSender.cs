using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace E_Commerce2.Servcies
{
    public class DevEmailSender
    {
        private readonly IConfiguration configuration;

        public DevEmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task SendEmailAsync(string emailTo, string subject, string htmlMessage)
        {
            var eamilSetting = new Email();

            configuration.GetSection("email").Bind(eamilSetting);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(eamilSetting.username, eamilSetting.ComapanyName);
            mail.To.Add(new MailAddress(emailTo));
            //if (!string.IsNullOrEmpty(bcc))
            //{
            //    mail.Bcc.Add(new MailAddress(bcc));
            //}
            //if (!string.IsNullOrEmpty(cc))
            //{
            //    mail.CC.Add(new MailAddress(cc));
            //}
            mail.Subject = subject;
            mail.Body = htmlMessage;
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = eamilSetting.smtpserver;
            client.Port = eamilSetting.port;
            client.EnableSsl = true;
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(eamilSetting.username, eamilSetting.password);
            try
            {
                await client.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Email
    {

        public string username { get; set; }
        public string password { get; set; }
        public string smtpserver { get; set; }
        public string mailAddress { get; set; }
        public int port { get;  set; }
        public string ComapanyName { get; set; }
    }
}

