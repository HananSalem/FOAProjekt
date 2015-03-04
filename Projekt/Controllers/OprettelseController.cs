using Projekt.DomainModels;
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

        //    if (collection.Get("handling") == "oprettelse")
        //    {
        //        return RedirectToAction("OpretMedarbejder", new { område = collection.Get("område") });
        //    }

        //    else
        //    {
        // return RedirectToAction("SletMedarbejder");
        //}
        //}

        //public ActionResult SletMedarbejder()
        //{
        //    return View("SletMedarbejder");


            return RedirectToAction("OpretMedarbejder", new { område = collection.Get("område") });
            
        }



        //Vis Opret Blanket
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

         Mail mail = new Mail();
         return mail.SendMail(id);

        }

        public int OpretBlanketIDB(FormCollection collection)
        {
            BlanketOprettelse blanket = new BlanketOprettelse(collection);
            int medarbejderId = blanket.Opret();
            return medarbejderId;    
        }


    }
}
