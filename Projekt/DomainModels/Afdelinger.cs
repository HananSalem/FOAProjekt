using FoaBrugerOprettelse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoaBrugerOprettelse.DomainModels
{
    public class Afdelinger
    {

        private BrugdataEntities brugdataDB = new BrugdataEntities();


        public List<string> hentAfdelinger(int område)
        {

            switch (område)
            {

                case 1: return hentAkasse();
                    break;
                case 2: return hentFaglig();
                    break;
                case 3: return hentStauning();
                    break;
                default: return null;

            }
        }



        public List<string> hentStauning()
        {

            List<string> afdelinger = brugdataDB.tbl_Manager.Where(t => t.Company == "FOA Staunings Plads").Select(t => t.Department).ToList();
            return afdelinger;

        }



        public List<string> hentAkasse()
        {

            List<string> afdelinger = brugdataDB.tbl_Manager.Where(t => t.Company != "FOA Staunings Plads" && t.Department.StartsWith("A-kasse")).Select(t => t.Department).ToList();
            return afdelinger;
        }


        public List<string> hentFaglig()
        {
            // Vi vælger at department ikke skal starte med "A-kasse" istedet for, at sige at den skal starte med "FOA". Da der er nogle enkelte faglige afdelinger i departments der ikke starter med FOA
            List<string> afdelinger = brugdataDB.tbl_Manager.Where(t => t.Company != "FOA Staunings Plads" && !(t.Department.StartsWith("A-kasse"))).Select(t => t.Department).ToList();
            return afdelinger;
        }








    }
}