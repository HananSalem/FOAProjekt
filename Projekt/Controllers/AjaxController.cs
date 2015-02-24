using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        private OprettelseDBEntities2 db = new OprettelseDBEntities2(); 

        public string GetAfdelingsNummer(string afdelingsnavn)
        {

           Afdeling afdeling = db.Afdeling.Find(afdelingsnavn);
           return Convert.ToString(afdeling.nummer);


        }





	}
}