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
    using System.ComponentModel.DataAnnotations;

    public partial class ADMIN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMIN()
        {
            this.tbl_category = new HashSet<tbl_category>();
        }
    
        public int AD_ID { get; set; }
        [Display(Name = "Admin Name")]
        [Required(ErrorMessage = "*")]

        public string AD_NAME { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]

        public string AD_PASSWORD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_category> tbl_category { get; set; }
    }
}
