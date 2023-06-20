//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int id { get; set; }
        public Nullable<int> ProductFK { get; set; }
        public Nullable<int> invoiceFK { get; set; }
        public Nullable<System.DateTime> datetime { get; set; }
        public Nullable<int> qty { get; set; }
        public Nullable<double> bill { get; set; }
        public Nullable<int> user_id_FK { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}