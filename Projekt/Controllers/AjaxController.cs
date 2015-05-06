using FoaBrugerOprettelse.DomainModels;
using FoaBrugerOprettelse.Models;
using FoaBrugerOprettelse.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoaBrugerOprettelse.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        private OprettelseDBEntities2 db = new OprettelseDBEntities2();
        private BrugdataEntities brugdataDB = new BrugdataEntities();
        private ADPerson ad = new ADPerson();

        
        public string HentLederPrAfdeling(string afdelingsnavn)
        {

            List<string> leder = brugdataDB.tbl_Manager.Where(t => t.Department == afdelingsnavn).Select(t => t.PersonaleAnsvarlig).ToList();
            if (leder[0] == null) {

             leder = brugdataDB.tbl_Manager.Where(t => t.Department == afdelingsnavn).Select(t => t.Manager).ToList();

            }
        
           return leder[0];
       
        }


        [HttpGet]
        public JsonResult HentLog()
        {
            List<Log> logs = db.Log.ToList();
            List<LogMapper> logMapper = new List<LogMapper>();
            foreach (Log log in logs)
            {
                LogMapper mapper = new LogMapper(log);
                logMapper.Add(mapper);
            }

            return Json(logMapper, JsonRequestBehavior.AllowGet);

        }



        public JsonResult Hent360Adgangsgrupper(string initialer)
        {
   
            List<string> grupper = ad.Hent360Adgangsgrupper(initialer);
            
            return Json(grupper, JsonRequestBehavior.AllowGet);

        }




        public JsonResult HentPostkasserOgGrupper(string afdeling, string område)
        {
           List<string> funktionspostkasser = ad.HentFunktionsPostkasser(Convert.ToInt32(område), afdeling);

            Adgangsgrupper adgangsgrupper = new Adgangsgrupper();
            List<string> grupper = adgangsgrupper.Hent360Adgangsgrupper(Convert.ToInt32(område), afdeling);

            Wrapper wrapper = new Wrapper(grupper, funktionspostkasser);

           return Json(wrapper, JsonRequestBehavior.AllowGet);

        }


    }
}