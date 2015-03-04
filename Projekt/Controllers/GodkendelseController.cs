using Projekt.DomainModels;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class GodkendelseController : Controller
    {

        private OprettelseDBEntities2 db = new OprettelseDBEntities2();


        [HttpGet]
        public ActionResult FindMedarbejder()
        {       
          Medarbejder medarbejder = db.Medarbejder.Find(Convert.ToInt32(this.Request.QueryString["medarbejderId"]));
            return View(medarbejder);
        }



        public ActionResult GodkendMedarbejder()
        {
            return View();       
        }

        public ActionResult Godkend(FormCollection collection)
        {
            Godkendelse godkendelse = new Godkendelse();
            godkendelse.GodkendMedarbejder(collection);
            return View();
        }






	}
}