﻿using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class OmrådeController : Controller
    {

        private OprettelseDBEntities2 db = new OprettelseDBEntities2();

        //
        // GET: /Område/
        public ActionResult VisOmråde()
        {
            
            return View(db.Område.ToList());
        }
	}
}