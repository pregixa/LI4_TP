//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackOffice
{
    using System;
    using System.Collections.Generic;
    
    public partial class Plano
    {
        public Plano()
        {
            this.Visita = new HashSet<Visita>();
        }
    
        public int id_plano { get; set; }
        public bool disponivel { get; set; }
        public Nullable<int> Fiscal { get; set; }
        public int FiscalCriador { get; set; }
    
        public virtual Fiscal Fiscal1 { get; set; }
        public virtual Fiscal Fiscal2 { get; set; }
        public virtual ICollection<Visita> Visita { get; set; }
    }
}
