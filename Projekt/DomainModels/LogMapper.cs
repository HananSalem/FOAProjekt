using FoaBrugerOprettelse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoaBrugerOprettelse.DomainModels
{
    public class LogMapper
    {

        public int områdeId {get; set;}
        public string medarbejderNavn { get; set; }
        public string medarbejderInitialer { get; set; } 
        public string status { get; set; }
        public string initialer { get; set; }
        public string dato { get; set; }

       


        public LogMapper(Log log)
        {
            Medarbejder medarbejder = log.Blanket.Medarbejder;

            områdeId =(int) medarbejder.område_id;
            medarbejderNavn = medarbejder.navn;
            medarbejderInitialer = medarbejder.initialer;
            status = log.status;
            initialer = log.initialer;
            dato = log.dato.ToShortDateString();

        }
    }
}