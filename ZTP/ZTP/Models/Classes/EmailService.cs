using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ZTP.Models.Classes
{
    public class EmailService
    {

        public async Task<bool> SendEmailAsync(string userEmail, string mailbody, string subject)
        {
            var from = new MailAddress("ticketserviceZTP@gmail.com");
            var to = new MailAddress(userEmail);


            using (var mail = new MailMessage(from, to))
            {
                mail.Subject = subject;
                mail.Body = mailbody;
                mail.IsBodyHtml = false;

                mail.ReplyToList.Add(from);
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay |
                                                   DeliveryNotificationOptions.OnFailure |
                                                   DeliveryNotificationOptions.OnSuccess;

                using (var client = new SmtpClient())
                {
                    await client.SendMailAsync(mail);
                }
            }

            return true;
        }
    }
}