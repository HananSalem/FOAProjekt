using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.DomainModels
{
    public class Godkendelse
    {
        private OprettelseDBEntities2 db = new OprettelseDBEntities2();

        public void GodkendMedarbejder(FormCollection collection)
        {
            int id = Convert.ToInt32(collection.Get("medarbejderId"));
            Log log = new Log();

            Blanket blanket = db.Blanket.Where(b => b.medarbejder_id == id).ToArray()[0];
            log.status = "Godkendt af leder";
            log.dato = DateTime.Today;
            log.Blanket = blanket;
            log.initialer = "cln01";
            db.Log.Add(log);

            db.SaveChanges();
           
        
        }



    }
}