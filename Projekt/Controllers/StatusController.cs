using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class StatusController : Controller
    {
        private OprettelseDBEntities2 db = new OprettelseDBEntities2();
        //
        // GET: /Status/
        public ActionResult GetStatus()
        {
            return View(db.Blanket.ToList());
        }
	}
}