//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quizz.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QUESTION
    {
        public int QUESTION_ID { get; set; }
        public string Q_TEXT { get; set; }
        public string OPA { get; set; }
        public string OPB { get; set; }
        public string OPC { get; set; }
        public string OPD { get; set; }
        public string COP { get; set; }
        public Nullable<int> q_fk_catid { get; set; }
    
        public virtual tbl_category tbl_category { get; set; }
    }
}
