//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class otscatlgos
    {
        public string cve_catlgo { get; set; }
        public string elm_cve { get; set; }
        public string nombre { get; set; }
    
        public virtual otsdcatlgos otsdcatlgos { get; set; }
    }
}
