using FoaBrugerOprettelse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FoaBrugerOprettelse.DomainModels
{
    public class Mail
    {

       public string body{get; set;}
       public string modtager { get; set; }
       public string emne { get; set; }


       public Mail()
       {
        
       }



       public string SendMail()
       {

           MailMessage mail = new MailMessage();
           mail.To.Add(modtager);
           mail.From = new MailAddress(Environment.UserName + "@foa.dk");
           mail.Subject = emne;
           mail.Body = body;
           mail.IsBodyHtml = true;
           SmtpClient smtp = new SmtpClient();
           smtp.Host = "foamail";
           smtp.Port = 25;
           smtp.UseDefaultCredentials = false;
 
           smtp.Send(mail);

           return "En mail er blivet sendt";
       }

       public string FindAkasseLederEmail(string afdeling)
       {
           BrugdataEntities brugdataDB = new BrugdataEntities();
          string email = brugdataDB.tbl_Manager.Where(t => t.Company == afdeling && t.Department.StartsWith("A-kasse")).Select(t => t.E_Mail).ToList()[0];
          return email;
       }

       public string FindLederEmail(string afdeling) {
           BrugdataEntities brugdataDB = new BrugdataEntities();
           // Først prøver vi at finde personale-ansvarligs mail
           List<string> email = brugdataDB.tbl_Manager.Where(t => t.Company == afdeling && !(t.Department.StartsWith("A-kasse"))).Select(t => t.PersAnsvarligMail).ToList();
           // Hvis der ikke findes en personale-ansvarlig tager vi bare lederens mail
           if (email[0] == null)
           {
               email = brugdataDB.tbl_Manager.Where(t => t.Company == afdeling && !(t.Department.StartsWith("A-kasse"))).Select(t => t.E_Mail).ToList();
           }
               return email[0];
       
       }

    }
}