using ImageCloudBLL.DTO;
using ImageCloudDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ImageCloudBLL.Infrastructure
{
    public  class Mailer
    {
        public void SendMail(User user)
        {
            MailAddress from = new MailAddress("ImageCloud9@gmail.com", "Tom");
            MailAddress to = new MailAddress(user.Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Тест";
            m.Body = string.Format(@"pEREIDITE PO SSILKE DLYA PODVERJDENIYA EMAILA <a href=http://localhost:50518/User/ConfirmEmail/{1} > Email Confirm</a>", user.UserName,user.Id);
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("ImageCloud9@gmail.com", "Sf2zEDDe3H4yGjA");
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Send(m);
        }
    }
}
