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
    
    public partial class Medarbejder_funktionspostkasse
    {
        public int medarbejder_id { get; set; }
        public string funktionspostkasse { get; set; }
    
        public virtual Medarbejder Medarbejder { get; set; }
    }
}
