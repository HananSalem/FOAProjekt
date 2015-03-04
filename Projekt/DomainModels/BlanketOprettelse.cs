using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.DomainModels
{
    public class BlanketOprettelse
    {

        private FormCollection collection;
        private OprettelseDBEntities2 db;


        public BlanketOprettelse(FormCollection collection)
        {
            this.collection = collection;
            this.db = new OprettelseDBEntities2();
        }


        public int Opret()
        {

            Medarbejder medarbejder = OpretMedarbejder();
            Blanket blanket = OpretBlanket(medarbejder);
            OpretLog(blanket);

            db.SaveChanges();
            return medarbejder.id;
        }

        private Medarbejder OpretMedarbejder()
        {

            Medarbejder medarbejder = new Medarbejder();

            medarbejder.navn = collection.Get("medarbejderNavn");
            medarbejder.afdelingsnavn = collection.Get("afdelinger");
            medarbejder.initialer = collection.Get("initialer");
            medarbejder.stillingsbetegnelse = collection.Get("stillingsbetegnelse");
            medarbejder.område_id = Convert.ToInt32(collection.Get("område"));
            medarbejder.cpr = collection.Get("cpr");
            medarbejder.fiks_id = Convert.ToInt32(collection.Get("fiks"));
            medarbejder.telefon = Convert.ToInt32(collection.Get("telefon"));
            medarbejder.start_dato = DateTime.ParseExact(collection.Get("datepicker"), "dd-MM-yyyy", null);
            List<string> values = collection.GetValues("a360").ToList();
            Collection<Grupper> grupper = new Collection<Grupper>();
            foreach (string value in values)
            {
                Grupper g = db.Grupper.Find(Convert.ToInt32(value));
                grupper.Add(g);
            }
            medarbejder.Grupper = grupper;

            List<string> values2 = collection.GetValues("postkasse").ToList(); // citrix

            Collection<Medarbejder_funktionspostkasse> postkasser = new Collection<Medarbejder_funktionspostkasse>();
            foreach (string value in values2)
            {
                Medarbejder_funktionspostkasse mf = new Medarbejder_funktionspostkasse();
                mf.funktionspostkasse = value;
                postkasser.Add(mf);
            }

            medarbejder.Medarbejder_funktionspostkasse = postkasser;

            List<string> values3 = collection.GetValues("udstyr").ToList();
            Collection<Udstyr> udstyr = new Collection<Udstyr>();
            foreach (string value in values3)
            {
                Udstyr u = db.Udstyr.Find(Convert.ToInt32(value));
                udstyr.Add(u);
            }

            medarbejder.Udstyr = udstyr;
            db.Medarbejder.Add(medarbejder);

            return medarbejder;
        }



        private Blanket OpretBlanket(Medarbejder medarbejder)
        {

            Blanket blanket = new Blanket();
            blanket.Medarbejder = medarbejder;
            blanket.type = "Oprettelse";

            db.Blanket.Add(blanket);
            return blanket;

        }


        private void OpretLog(Blanket blanket)
        {
            Log log = new Log();
            log.status = "Afventer godkendelse";
            log.dato = DateTime.Today;
            log.initialer = "hsal";
            log.Blanket = blanket;

            db.Log.Add(log);

            

        }


    }
}