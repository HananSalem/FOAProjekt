using FoaBrugerOprettelse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoaBrugerOprettelse.Controllers
{
    public class LogController : Controller
    {
        private OprettelseDBEntities2 db = new OprettelseDBEntities2();
        //
        // GET: /Status/
        public ActionResult VisLog()
        {
            List<Område> område = db.Område.ToList();
            List<Log> log = db.Log.ToList();

            LogData logData = new LogData(område, log);

            return View(logData);
        }


	}
}