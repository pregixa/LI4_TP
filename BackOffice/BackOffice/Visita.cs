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
    
    public partial class Visita
    {
        public Visita()
        {
            this.Foto = new HashSet<Foto>();
            this.Nota = new HashSet<Nota>();
            this.VisitaQuestao = new HashSet<VisitaQuestao>();
            this.Voz = new HashSet<Voz>();
        }
    
        public int id_vis { get; set; }
        public System.DateTime dataVisita { get; set; }
        public int plano { get; set; }
        public int estabelecimento { get; set; }
        public Nullable<System.DateTime> dataRelatorio { get; set; }
        public string classificacao { get; set; }
        public string html_file { get; set; }
        public bool concluido { get; set; }
    
        public virtual Estabelecimento Estabelecimento1 { get; set; }
        public virtual ICollection<Foto> Foto { get; set; }
        public virtual ICollection<Nota> Nota { get; set; }
        public virtual Plano Plano1 { get; set; }
        public virtual ICollection<VisitaQuestao> VisitaQuestao { get; set; }
        public virtual ICollection<Voz> Voz { get; set; }
    }
}
