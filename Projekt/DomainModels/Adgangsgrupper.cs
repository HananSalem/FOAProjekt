using FoaBrugerOprettelse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoaBrugerOprettelse.DomainModels
{
    public class Adgangsgrupper
    {
        private BrugdataEntities brugdataDB = new BrugdataEntities();

        public List<string> Hent360Adgangsgrupper(int område, string afdeling)
        {

            switch (område)
            {

                case 1: return hentAkasse();
                    break;
                case 2: return hentFaglig(afdeling);
                    break;
                case 3: return hentStauning(afdeling);
                    break;
                default: return null;

            }

        }


        public List<string> hentFaglig(string afdeling)
        {

            List<string> grupper = brugdataDB.tbl_360Groups.Where(t => t.Department == afdeling || t.Department == "Alle").Select(t => t.Groups360).ToList();
            return grupper;


        }


        public List<string> hentAkasse()
        {

            List<string> grupper = brugdataDB.tbl_360Groups.Where(t => t.Department == "Alle").Select(t => t.Groups360).ToList();
            return grupper;


        }



        public List<string> hentStauning(string afdeling)
        {

            List<string> grupper;

            if (afdeling.StartsWith("A-kasse"))
            {

                grupper = brugdataDB.tbl_360Groups.Where(t => t.Department == "Alle" ||
                    t.Department == "FOA Staunings Plads" ||
                    t.Department == "A-kasse FOA Staunings Plads" ||
                    t.Department == "A-kasse FOA Stauning Plads"
                    ).Select(t => t.Groups360).ToList();

            }

            else
            {

                grupper = brugdataDB.tbl_360Groups.Where(t => t.Department == "Alle" ||
                    t.Department == "FOA Staunings Plads" ||
                    t.Department == "Faglig FOA Staunings Plads").Select(t => t.Groups360).ToList();

            }
            return grupper;

        }

    }
}
