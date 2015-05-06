using FoaBrugerOprettelse.DomainModels;
using FoaBrugerOprettelse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoaBrugerOprettelse.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/


        private OprettelseDBEntities2 db = new OprettelseDBEntities2();


        public ActionResult Home()
        {

            ADPerson ap = new ADPerson();
            bool result = ap.TjekOmLoginErLeder();
           
            return View(result);
        }


        public ActionResult Index(FormCollection collection) 
        {
            string request = collection.Get("action");

            if (request == "opret")
            {
              
                return RedirectToAction("VisOprettelsesBlanket","Oprettelse" , new { område = collection.Get("område") });
            }


            else
            {
                string id = this.Request.QueryString["medarbejderId"];
                return RedirectToAction("VisGodkendelsesView", "Godkendelse", new { id = id });

            }   
        }

    

        public ActionResult Action(FormCollection collection)
        {
            string request = collection.Get("send");

            TempData["collection"] = collection;

            if (request == "Send til godkendelse")
            {
                //Hvis det er fiks kode 3 skal der sendes en godkendelsesmail til A-kasse lederen
                if (Convert.ToInt32(collection.Get("fiks")) == 2)
                {
                    return RedirectToAction("SendGodkendelsesMailTilAkasseLeder", "Oprettelse");
                }
                else
                {

                    return RedirectToAction("SendGodkendelsesMailTilLeder", "Oprettelse");

                }
                           
            }


            else
            {

                return RedirectToAction("GodkendMedarbejder", "Godkendelse");

            }   
            
        }




        public ActionResult VisPostkasser()
        {
            ADPerson ad = new ADPerson();
            return View(ad.HentFunktionsPostkasser(3, "FOA Faglig"));

        }


	}
}