using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoaBrugerOprettelse.ViewModels
{
    public class Wrapper
    {
        List<string> grupper;

        public List<string> Grupper
        {
            get { return grupper; }
            set { grupper = value; }
        }
        List<string> postkasser;

        public List<string> Postkasser
        {
            get { return postkasser; }
            set { postkasser = value; }
        }

        public Wrapper(List<string> grupper, List<string> postkasser) {

            this.grupper = grupper;
            this.postkasser = postkasser;
        }


    }
}