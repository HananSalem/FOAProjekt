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

            string id = this.Request.QueryString["medarbejderId"];
            Medarbejder medarbejder =  db.Medarbejder.Find(Convert.ToInt32(id));

            return View(medarbejder);
        }



        public ActionResult GodkendMedarbejder()
        {
            return View();

          
        }

        public ActionResult Godkend(FormCollection collection)
        {
            int id = Convert.ToInt32(collection.Get("medarbejderId"));
            Blanket blanket = db.Blanket.Where(b => b.medarbejder_id == id).ToArray()[0];
            blanket.status = "Godkendt af leder";
            db.SaveChanges();
            return View();
        }






	}
}