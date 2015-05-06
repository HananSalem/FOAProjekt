using FoaBrugerOprettelse.DomainModels;
using FoaBrugerOprettelse.Models;
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

namespace FoaBrugerOprettelse.Controllers
{
    public class OprettelseController : Controller
    {
        private OprettelseDBEntities2 db = new OprettelseDBEntities2();
        private BrugdataEntities brugdataDB = new BrugdataEntities();
        ADPerson ad = new ADPerson();



        // GET: /Afdeling/
        public ActionResult Index(FormCollection collection)
        {

            return RedirectToAction("VisOprettelsesBlanket", new { område = collection.Get("område") });
        }


        public ActionResult VisOprettelsesBlanket()
        {
            int områdeId = Convert.ToInt32(this.Request.QueryString["område"]);

            // henter afdelinger fra brugdata
            Afdelinger afd = new Afdelinger();
            List<string> afdelinger = afd.hentAfdelinger(områdeId);
    
            Område o = db.Område.Find(områdeId);

            List<Fiks> fiks = db.Fiks.Where(f => f.område_id == områdeId).ToList();
            // List<Grupper> adgangsgrupper = db.Grupper.Where(g => g.område_id == områdeId || g.område_id == null).ToList();
            List<Udstyr> udstyr = db.Udstyr.ToList();

            BlanketViewModel blanketViewModel = new BlanketViewModel(afdelinger, fiks, udstyr, o);



            return View("Blanket", blanketViewModel);
        }



        public ActionResult VisOprettelsesBlanketPersonale()
        {
            int områdeId = Convert.ToInt32(this.Request.QueryString["område"]);

            // henter afdelinger fra brugdata
            Afdelinger afd = new Afdelinger();
            List<string> afdelinger = afd.hentAfdelinger(områdeId);

            Område o = db.Område.Find(områdeId);

            BlanketViewModel blanketViewModel = new BlanketViewModel(afdelinger, o);
            return View("OprettelsePersonale", blanketViewModel);

        }


        public ActionResult SendGodkendelsesMailTilAkasseLeder()
        {
            FormCollection collection = (FormCollection)TempData["collection"];

            int id = OpretBlanketIDB(collection);

            Mail mail = new Mail();
            mail.modtager = "irar@foa.dk";
            mail.body = mail.FindAkasseLederEmail(collection.Get("afdelinger")) + " - Tryk venligst på nedenstående link, for at godkende den nye medarbejder\n" + "http://localhost:59312/Home/Index?medarbejderId=" + id;
            mail.emne = "Godkend ny medarbejder";
            ViewBag.Besked = mail.SendMail();
            return View("VisBesked");

        }

        public ActionResult SendGodkendelsesMailTilLeder()
        {
            FormCollection collection = (FormCollection)TempData["collection"];

            int id = OpretBlanketIDB(collection);

            Mail mail = new Mail();
            //mail.modtager = collection.Get("leder");
            mail.modtager = "irar@foa.dk";
            mail.body = mail.FindLederEmail(collection.Get("afdelinger")) + " - Tryk venligst på nedenstående link, for at godkende den nye medarbejder\n" + "http://localhost:59312/Home/Index?medarbejderId=" + id;
            mail.emne = "Godkend ny medarbejder";
            ViewBag.Besked = mail.SendMail();
            return View("VisBesked");
        }

        private int OpretBlanketIDB(FormCollection collection)
        {
            
            BlanketOprettelse blanket = new BlanketOprettelse(collection);
            int medarbejderId = blanket.Opret();

            return medarbejderId;
        
        }

        public ActionResult OpretBlanket()
        {
            FormCollection collection = (FormCollection)TempData["collection"];
            OpretBlanketIDB(collection);
            ViewBag.Besked = "Medarbejderen er oprettet i databasen";
            return View("VisBesked");

        }

        public ActionResult OpretPersonoplysninger(FormCollection collection)
        {

            BlanketOprettelse blanket = new BlanketOprettelse(collection);
            int medarbejderId = blanket.OpretPersonoplysninger();
            Mail mail = new Mail();
            mail.modtager = "irar@foa.dk";
            mail.body = mail.FindLederEmail(collection.Get("afdelinger")) + "Tryk venligst på nedenstående link, for at oprette den nye medarbejder \n" + "http://localhost:59312/Oprettelse/VisPersonoplysninger?medarbejderId=" + medarbejderId;
            mail.emne = "Ny medarbejder";

            ViewBag.Besked = mail.SendMail(); 
            return View("VisBesked");
        }


        // Viser oprettelsesblanket med personligoplysininger fra personale.
        public ActionResult VisPersonoplysninger()
        {

            string id = this.Request.QueryString["medarbejderId"];
            Medarbejder medarbejder = db.Medarbejder.Find(Convert.ToInt32(id));

            int områdeId = (int)medarbejder.område_id;

            Afdelinger afd = new Afdelinger();
            List<string> afdelinger = afd.hentStauning();

            List<Fiks> fiks = db.Fiks.Where(f => f.område_id == områdeId).ToList();
         List<string> adgangsgrupper = ad.Hent360Adgangsgrupper();
            Adgangsgrupper ag = new Adgangsgrupper();
          //List<string> adgangsgrupper = ag.Hent360Adgangsgrupper(områdeId, medarbejder.afdelingsnavn);  // erstattes med det her?


            List<Udstyr> udstyr = db.Udstyr.ToList();

            BlanketViewModel blanketViewModel = new BlanketViewModel(medarbejder, afdelinger, fiks, adgangsgrupper, udstyr);
            return View("Blanket", blanketViewModel);

        }


        public JsonResult TjekInitialer(string initialer)
        {

            ADPerson ad = new ADPerson();
            bool adResult = ad.TjekInitialerAD(initialer);
            T_initialer initiale = brugdataDB.T_initialer.Find(initialer);

            if (initiale == null && adResult == true)
            {

                return Json(true, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }

        }



    }
}
