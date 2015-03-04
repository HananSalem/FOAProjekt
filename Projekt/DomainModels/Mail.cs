using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Projekt.DomainModels
{
    public class Mail
    {

     

        public string SendMail(int id)
        {

            MailMessage mail = new MailMessage();
            mail.To.Add("irar@foa.dk");
            mail.From = new MailAddress("hsal@foa.dk");
            mail.Subject = "Godkend ny medarbejder";
            string Body = "Tryk venligst på nedenstående link, for at godkende den nye medarbejder\n" + "http://localhost:59312/Godkendelse/FindMedarbejder?medarbejderId=" + id + "&ok=OK";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "foamail";
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("hsal@foa.dk", "henuhenu");

            smtp.Send(mail);


            return "En mail er blivet sent til lederen";
        }


    }
}