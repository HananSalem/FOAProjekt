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
    
    public partial class Blanket
    {
        public Blanket()
        {
            this.Log = new HashSet<Log>();
        }
    
        public int id { get; set; }
        public int medarbejder_id { get; set; }
        public string type { get; set; }
    
        public virtual Medarbejder Medarbejder { get; set; }
        public virtual ICollection<Log> Log { get; set; }
    }
}
