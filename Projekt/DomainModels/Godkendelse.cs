using FoaBrugerOprettelse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoaBrugerOprettelse.DomainModels
{
    public class Godkendelse
    {
        private OprettelseDBEntities2 db = new OprettelseDBEntities2();

        public void GodkendMedarbejder(int medarbejderId)
        {
            Log log = new Log();

            Blanket blanket = db.Blanket.Where(b => b.medarbejder_id == medarbejderId).ToArray()[0];
            log.status = "Godkendt af leder";
            log.dato = DateTime.Today;
            log.Blanket = blanket;
            log.initialer = Environment.UserName;
            db.Log.Add(log);

            db.SaveChanges();
           
        }





    }
}