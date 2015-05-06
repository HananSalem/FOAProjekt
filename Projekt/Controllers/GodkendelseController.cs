using FoaBrugerOprettelse.DomainModels;
using FoaBrugerOprettelse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoaBrugerOprettelse.Controllers
{
    public class GodkendelseController : Controller
    {

        private OprettelseDBEntities2 db = new OprettelseDBEntities2();
        ADPerson ad = new ADPerson();

        [HttpGet]
        public ActionResult VisGodkendelsesView(string id)
        {  
     
          Medarbejder medarbejder = db.Medarbejder.Find(Convert.ToInt32(id));
          System.Diagnostics.Debug.Write("godkendelsecontroller "+medarbejder.navn);


          int områdeId = (int)medarbejder.område_id;

          Afdelinger afd = new Afdelinger();
          List<string> afdelinger = afd.hentAfdelinger(områdeId);

          List<Fiks> fiks = db.Fiks.Where(f => f.område_id == områdeId).ToList();
          Adgangsgrupper ag = new Adgangsgrupper();
          List<string> adgangsgrupper = ag.Hent360Adgangsgrupper(områdeId, medarbejder.afdelingsnavn);
           
          // List<string> adgangsgrupper = ad.Hent360Adgangsgrupper();
          List<Udstyr> udstyr = db.Udstyr.ToList();

          List<string> funktionspostkasser = ad.HentFunktionsPostkasser(områdeId, medarbejder.afdelingsnavn);

          BlanketViewModel blanketViewModel = new BlanketViewModel(medarbejder, afdelinger, fiks, adgangsgrupper, udstyr, områdeId, funktionspostkasser);
            return View("Blanket", blanketViewModel);
        }



        public ActionResult GodkendMedarbejder()
        {
            FormCollection collection = (FormCollection)TempData["collection"];
            Godkendelse godkendelse = new Godkendelse();
            BlanketOprettelse b = new BlanketOprettelse(collection);
            b.Update();
            godkendelse.GodkendMedarbejder(Convert.ToInt32(collection.Get("medarbejderId")));

            return View();
        }



}


}
    
