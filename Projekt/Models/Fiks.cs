//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoaBrugerOprettelse.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fiks
    {
        public Fiks()
        {
            this.Medarbejder = new HashSet<Medarbejder>();
        }
    
        public int id { get; set; }
        public string kode { get; set; }
        public int område_id { get; set; }
        public string beskrivelse { get; set; }
    
        public virtual Område Område { get; set; }
        public virtual ICollection<Medarbejder> Medarbejder { get; set; }
    }
}
