using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoaBrugerOprettelse.Models
{
    public class BlanketViewModel
    {

        public string id { get; set; }
        public string navn { get; set; }
        public string initialer { get; set; }
        public string stillingsbetegnelse { get; set; }
        public string telefon { get; set; }
        public string afdelingsnavn { get; set; }
        public string område { get; set; }
        public string cpr { get; set; }
        public int fiks_id { get; set; }
        public string start_dato { get; set; }
        public int områdeId{get; set;}

        public string knapTekst { set; get; }
        public string action { set; get; }

        public List<int> udstyrId { get; set; }
        public List<string> funktionspostkasser { get; set; }
        public List<string> medarbGrupper { get; set; }
        public List<string> medarbejder_funktionspostkasser { get; set; }

        public List<string> afdelinger { get; set; }
        public List<string> adgangsgrupper { get; set; }
        public List<Fiks> fiks { get; set; }
        public List<Udstyr> udstyr { get; set; }



        public BlanketViewModel(Medarbejder medarbejder, List<string> afdelinger, List<Fiks> fiks, List<string> adgangsgrupper, List<Udstyr> udstyr, int område, List<string> funktionspostkasser)
        {
            this.id = medarbejder.id.ToString();
            this.navn = medarbejder.navn;

            System.Diagnostics.Debug.Write(navn);

            this.initialer = medarbejder.initialer;
            this.stillingsbetegnelse = medarbejder.stillingsbetegnelse;
            this.telefon = medarbejder.telefon.ToString();
            this.afdelingsnavn = medarbejder.afdelingsnavn;
            this.område = medarbejder.Område.navn;
            this.cpr = medarbejder.cpr;
            this.fiks_id = (int)medarbejder.fiks_id;
            this.start_dato = medarbejder.start_dato.ToString();


            knapTekst = "Godkend medarbejder";
            action = "Godkend";

          
            this.adgangsgrupper = adgangsgrupper;
            this.afdelinger = afdelinger;
            this.fiks = fiks;
            this.udstyr = udstyr;
            this.områdeId = område;

            medarbGrupper = new List<string>();
            foreach(Grupper grupper in medarbejder.Grupper){
                medarbGrupper.Add(grupper.Gruppe);
            }


            udstyrId = new List<int>();
            foreach (Udstyr udstyrr in medarbejder.Udstyr)
            {
                udstyrId.Add(udstyrr.id);
            }

            this.medarbejder_funktionspostkasser = new List<string>();

            foreach (Medarbejder_funktionspostkasse postkasse in medarbejder.Medarbejder_funktionspostkasse)
            {
                medarbejder_funktionspostkasser.Add(postkasse.funktionspostkasse);
            }      


            this.funktionspostkasser = funktionspostkasser;

        }



        public BlanketViewModel(Medarbejder medarbejder, List<string> afdelinger, List<Fiks> fiks, List<string> adgangsgrupper, List<Udstyr> udstyr)
        {
            this.id = medarbejder.id.ToString();
            this.navn = medarbejder.navn;
            this.initialer = medarbejder.initialer;
            this.stillingsbetegnelse = medarbejder.stillingsbetegnelse;
            this.afdelingsnavn = medarbejder.afdelingsnavn;
            this.område = medarbejder.Område.navn;
            this.cpr = medarbejder.cpr;
           
            this.start_dato = medarbejder.start_dato.ToString();


            knapTekst = "Godkend medarbejder";
            action = "Godkend";


            this.adgangsgrupper = adgangsgrupper;
            this.afdelinger = afdelinger;
            this.fiks = fiks;
            this.udstyr = udstyr;
            this.områdeId = medarbejder.Område.id;      
        
        }

        public BlanketViewModel(List<string> afdelinger, List<Fiks> fiks, List<Udstyr> udstyr, Område område)
        {

      
            this.afdelinger = afdelinger;
            this.fiks = fiks;
            this.udstyr = udstyr;
            this.områdeId = område.id;
            this.område = område.navn;

            action = "Opret";
            knapTekst = "Send til godkendelse";
        }




        public BlanketViewModel(List<string> afdelinger, Område område)
        {
            this.afdelinger = afdelinger;
            this.område = område.navn;
            this.områdeId = område.id;

        }
        





    }
}