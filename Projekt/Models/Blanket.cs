//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projekt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Blanket
    {
        public int id { get; set; }
        public string status { get; set; }
        public Nullable<int> leder_id { get; set; }
        public int medarbejder_id { get; set; }
    
        public virtual Leder Leder { get; set; }
        public virtual Medarbejder Medarbejder { get; set; }
    }
}
