//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
        public string UserId { get; set; }
        public int BillTypeId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual BillType BillType { get; set; }
    }
}