using Projekt.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class OprettelseController : Controller
    {
        private OprettelseDBEntities2 db = new OprettelseDBEntities2();

        int områdeId;

        // GET: /Afdeling/
        public ActionResult Index(FormCollection collection)
        {

            if (collection.Get("handling") == "oprettelse")
            {
                return RedirectToAction("OpretMedarbejder", new { område = collection.Get("område") });
            }

            else
            {
         return RedirectToAction("SletMedarbejder");
        }
          

        }

        public ActionResult SletMedarbejder()
        {
            return View("SletMedarbejder");
            
        }

        public string Slet(FormCollection collection)
        {
            string init = collection.Get("init");
            Medarbejder medarbejder = db.Medarbejder.Where(m => m.initialer == init).ToArray()[0];
            medarbejder.slut_dato = DateTime.ParseExact(collection.Get("datepicker"), "dd-MM-yyyy", null);
    
            db.SaveChanges();
            return "Slet Metoden";
            
        }




        public ActionResult OpretMedarbejder(String område)
        {

            områdeId = Convert.ToInt32(område);

            List<Afdeling> afdelinger = db.Afdeling.ToList();
            List<Fiks> fiks = db.Fiks.Where(f => f.område_id == områdeId).ToList();
            List<Grupper> adgangsgrupper = db.Grupper.Where(g => g.område_id == områdeId || g.område_id == null).ToList();
            List<Udstyr> udstyr = db.Udstyr.ToList();

            Data data = new Data(afdelinger, fiks, adgangsgrupper, udstyr, områdeId);


            return View(data);
        }


        public string SendMail(FormCollection collection)
        {

           int id = OpretBlanketIDB(collection);

            MailMessage mail = new MailMessage();
            mail.To.Add("irar@foa.dk");
            mail.From = new MailAddress("hsal@foa.dk");
            mail.Subject = "Godkend ny medarbejder";
            string Body = "Tryk venligst på nedenstående link, for at godkende den nye medarbejder\n" + "http://localhost:59312/Godkendelse/FindMedarbejder?medarbejderId=" + id +"&ok=OK";
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

        public int OpretBlanketIDB(FormCollection collection)
        {

            Blanket b = new Blanket();
            b.status = "Afventer godkendelse";
            Medarbejder m = new Medarbejder();

            m.navn = collection.Get("medarbejderNavn");
            m.afdelingsnavn = collection.Get("afdelinger");
            m.initialer = collection.Get("initialer");
            m.stillingsbetegnelse = collection.Get("stillingsbetegnelse");
            m.område_id = Convert.ToInt32(collection.Get("område"));
            m.cpr = collection.Get("cpr");
            m.fiks_id = Convert.ToInt32(collection.Get("fiks"));
            m.telefon = Convert.ToInt32(collection.Get("telefon"));
            m.start_dato = DateTime.ParseExact(collection.Get("datepicker"), "dd-MM-yyyy", null);
            List<string> values = collection.GetValues("a360").ToList();
            Collection<Grupper> grupper = new Collection<Grupper>();
            foreach(string value in values)
            {
                  Grupper g = db.Grupper.Find(Convert.ToInt32(value));
                  grupper.Add(g);
            }
            m.Grupper =grupper;

            List<string> values2 = collection.GetValues("postkasse").ToList(); // citrix

            Collection<Medarbejder_funktionspostkasse> postkasser = new Collection<Medarbejder_funktionspostkasse>();
            foreach (string value in values2)
            {
                Medarbejder_funktionspostkasse mf = new Medarbejder_funktionspostkasse();
                mf.funktionspostkasse = value;
                postkasser.Add(mf);
            }

            m.Medarbejder_funktionspostkasse = postkasser;

            List<string> values3 = collection.GetValues("udstyr").ToList();
            Collection<Udstyr> udstyr = new Collection<Udstyr>();
            foreach (string value in values3)
            {
                Udstyr u = db.Udstyr.Find(Convert.ToInt32(value));
                udstyr.Add(u);
            }

            m.Udstyr = udstyr;


            b.Medarbejder = m;
            db.Medarbejder.Add(m);
            db.Blanket.Add(b);
            db.SaveChanges();
            return m.id;
        }








    }
}
