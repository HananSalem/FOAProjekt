using FoaBrugerOprettelse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoaBrugerOprettelse.DomainModels
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

        public int OpretPersonoplysninger()
        {
            Medarbejder medarbejder = OpretMedarbejderPersonoplysninger(new Medarbejder());
           
            Blanket blanket = OpretBlanket(medarbejder);

            OpretLog(blanket, "Afventer godkendelse");

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
           

            return medarbejder.id;
        
        }

       

        public int Opret()
        {

            Medarbejder medarbejder = OpretMedarbejder(new Medarbejder());
           
            Blanket blanket = OpretBlanket(medarbejder);

          
            db.Medarbejder.Add(medarbejder);



            OpretLog(blanket, "Afventer godkendelse");

            db.SaveChanges();
      
            return medarbejder.id;
        }

        public void Update()
        {
            int medarbejderId = Convert.ToInt32(collection.Get("medarbejderId"));
            Medarbejder medarbejder = OpretMedarbejder(db.Medarbejder.Find(medarbejderId));

            OpretLog(medarbejder.Blanket.ToList()[0], "Godkendt af Leder");
            db.SaveChanges();
        }
    

        private Medarbejder OpretMedarbejderPersonoplysninger(Medarbejder medarbejder) {

            medarbejder.navn = collection.Get("medarbejderNavn");
            medarbejder.afdelingsnavn = collection.Get("afdelinger");
            medarbejder.initialer = collection.Get("initialer");
            medarbejder.stillingsbetegnelse = collection.Get("stillingsbetegnelse");
            medarbejder.område_id = Convert.ToInt32(collection.Get("områdeId"));
         
            medarbejder.start_dato = DateTime.ParseExact(collection.Get("datepicker").ToString(), "d", null);

            return medarbejder;

        }


        private Medarbejder OpretMedarbejder(Medarbejder medarbejder)
        {

            medarbejder.navn = collection.Get("medarbejderNavn");
            medarbejder.afdelingsnavn = collection.Get("afdelinger");
            medarbejder.initialer = collection.Get("initialer");
            medarbejder.stillingsbetegnelse = collection.Get("stillingsbetegnelse");
            medarbejder.område_id = Convert.ToInt32(collection.Get("område"));
            medarbejder.cpr = collection.Get("cpr");
            medarbejder.fiks_id = Convert.ToInt32(collection.Get("fiks"));
            medarbejder.telefon = Convert.ToInt32(collection.Get("telefon"));
          
            medarbejder.start_dato =  DateTime.ParseExact(collection.Get("datepicker").ToString(), "d", null);

            List<string> grupperList = collection.GetValues("a360").ToList();
            List<Grupper> grupper = new List<Grupper>();

            foreach (string gruppe in grupperList)
            {
                Grupper g = new Grupper();
                g.Medarbejder_id = medarbejder.id;
                g.Gruppe = gruppe;
                grupper.Add(g);
            }

            medarbejder.Grupper.Clear();
            medarbejder.Grupper = grupper;


            List<string> postkasserList = collection.GetValues("postkasse").ToList();

            Collection<Medarbejder_funktionspostkasse> postkasser = new Collection<Medarbejder_funktionspostkasse>();
            foreach (string postkasse in postkasserList)
            {
                Medarbejder_funktionspostkasse medarbejderPostkasse = new Medarbejder_funktionspostkasse();
                medarbejderPostkasse.funktionspostkasse = postkasse;
                postkasser.Add(medarbejderPostkasse);
            }

            medarbejder.Medarbejder_funktionspostkasse.Clear();
            medarbejder.Medarbejder_funktionspostkasse = postkasser;

            //List<string> specialprogrammer = collection.GetValues("specialeprogrammer").ToList();
            //Collection



            List<string> udstyrList = collection.GetValues("udstyr").ToList();
            Collection<Udstyr> udstyr = new Collection<Udstyr>();
            foreach (string item in udstyrList)
            {
                Udstyr medarbejderUdstyr = db.Udstyr.Find(Convert.ToInt32(item));
                udstyr.Add(medarbejderUdstyr);
            }
            medarbejder.Udstyr.Clear();
            medarbejder.Udstyr = udstyr;
            

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


        private void OpretLog(Blanket blanket, string status)
        {
            Log log = new Log();
            log.status = status;
            log.dato = DateTime.Today;
            log.initialer = Environment.UserName;
            log.Blanket = blanket;

            db.Log.Add(log);

        }


    }
}