using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ReposatoryLayer.Service
{
    public class EmailServices
    {
        public static void SendMail(string email, string token)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("ssah94183@gmail.com", "pfcfjzdklwizukay");
                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(email);
                msgObj.From = new MailAddress("ssah94183@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.IsBodyHtml = true;
                //  msgObj.Body = $"localhost:4200/reset-password/{token}";

                msgObj.Body = "<html><body><p><b>Hello </b>,<br/>click the below link for reset password.<br/>" +
                   $"http://localhost:4200/reset-password/{token}" +
                   "</p></body></html>";

                client.Send(msgObj);
            }
        }
    }
}
