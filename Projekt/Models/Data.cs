using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Data
    {

        private List<Afdeling> afdelinger;
        private List<Grupper> adgangsgrupper;
        private List<Fiks> fiks;
        private List<Udstyr> udstyr;
        private int område;

        public int Område
        {
            get { return område; }
            set { område = value; }
        }


        public List<Afdeling> Afdelinger
        {
            get { return afdelinger; }
            set { afdelinger = value; }
        }
      
        public List<Fiks> Fiks
        {
            get { return fiks; }
            set { fiks = value; }
        }

        public List<Grupper> Adgangsgrupper
        {
            get { return adgangsgrupper; }
            set { adgangsgrupper = value; }
        }

        public List<Udstyr> Udstyr
        {
            get { return udstyr; }
            set { udstyr = value; }
        }


        public Data(List<Afdeling> afdelinger, List<Fiks> fiks, List<Grupper> adgangsgrupper, List<Udstyr> udstyr, int område)
        {
            this.afdelinger = afdelinger;
            this.fiks = fiks;
            this.adgangsgrupper = adgangsgrupper;
            this.udstyr = udstyr;
            this.område = område;
        }
    }
}