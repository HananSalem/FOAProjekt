using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoaBrugerOprettelse.Models
{
    public class LogData
    {

        private List<Område> område;
        private List<Log> log;


        public LogData(List<Område> område, List<Log> log)
        {
            this.område = område;
            this.log = log;
        }


        public List<Log> Log
        {
            get { return log; }
            set { log = value; }
        }


        public List<Område> Område
        {
            get { return område; }
            set { område = value; }
        }


    }
}