//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Carubbi.ControleEstoque.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sales
    {
        public int Id { get; set; }
        public Nullable<int> StudentId { get; set; }
        public int ProductId { get; set; }
        public System.DateTime SaleDate { get; set; }
        public decimal Price { get; set; }
        public int AcademyId { get; set; }
    
        public virtual Products Products { get; set; }
        public virtual Students Students { get; set; }
        public virtual Academies Academies { get; set; }
    }
}
